using AutoMapper;
using Csla;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Models.Users;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.Framework.Exceptions;
using SurgeonPortal.DataAccess.Contracts.Users;
using System.IdentityModel.Tokens.Jwt;
using Ytg.Framework.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ytg.AspNetCore.Configuration;
using Microsoft.Extensions.Options;
using SurgeonPortal.Library.Users;
using SurgeonPortal.Shared;

namespace SurgeonPortal.Api.Controllers.Users
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("v{version:apiVersion}/users")]
	public class UsersController : YtgControllerBase
	{
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly TokensConfiguration _tokensConfiguration;
        private readonly IUserTokenFactory _userTokenFactory;
        private readonly IIdentityProvider _identityProvider;

        public UsersController(IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IOptions<TokensConfiguration> tokensConfiguration,
            IUserTokenFactory userTokenFactory,
            IIdentityProvider identityProvider)
            : base(webHostEnvironment)
        {
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<UsersController>();
            _userTokenFactory = userTokenFactory;
            _identityProvider = identityProvider;
            _tokensConfiguration = tokensConfiguration.Value;
        }

        [AllowAnonymous]
        [MapToApiVersion("1")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("authenticate")]
        public async Task<ActionResult> GetUserToken([FromServices] IAppUserReadOnlyFactory appUserReadOnlyFactory,
            [FromBody] UserCredentialModel model)
        {
            try
            {
                var user = await appUserReadOnlyFactory.GetByCredentialsAsync(
                    model.EmailAddress,
                    model.Password);

                _logger.LogInformation($"User authenticated successfully. User: {user.UserId}");

                var claims = GetClaimsFromUser(user);

                return await GenerateTokenAsync(user, claims);
            }
            catch (DataPortalException ex) when (ex.BusinessException is AuthenticationFailedException)
            {
                _logger.LogInformation($"GetUserToken(): User failed authentication. ({model.EmailAddress})");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetUserToken(): {ex}");
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("test-auth")]
        public IActionResult TestAuth()
        {
            if(User.Identity != null)
            {
                return Ok($"Authorized as {User.Identity.Name}.");
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [MapToApiVersion("1")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("refresh")]
        public async Task<IActionResult> GetTokenViaRefresh([FromServices] IAppUserReadOnlyFactory appUserReadOnlyFactory,
            [FromBody] RefreshTokenModel model)
        {
            try
            {
                var user = await appUserReadOnlyFactory.GetByTokenAsync(model.RefreshToken);

                _logger.LogInformation($"User successfully authenticated. (refreshToken[{model.RefreshToken}])");

                var claims = GetClaimsFromUser(user);

                return await GenerateTokenAsync(user, claims);
            }
            catch (DataPortalException ex) when (ex.BusinessException is AuthenticationFailedException)
            {
                _logger.LogInformation($"GetTokenViaRefresh(): Refresh token not found or is expired. (refreshToken[{model.RefreshToken}])");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetTokenViaRefresh(): {ex}");
                return BadRequest();
            }
        }

        private async Task<ActionResult> GenerateTokenAsync(IAppUserReadOnly user, List<Claim> claims)
        {
            var allClaims =
                new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.EmailAddress),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.GivenName, user.FullName),
                };

            if (user != null)
            {
                allClaims.Add(new Claim(YtgClaimType.UserId, user.UserId.ToString()));
            }
            allClaims.AddRange(claims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokensConfiguration.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var refreshToken = Guid.NewGuid().ToString();
            var refreshExpiration = DateTime.Now.AddDays(7);

            var expiresAt = DateTime.Now.AddMinutes(_tokensConfiguration.ExpirationInMinutes + 1);

            if (user != null)
            {
                var userToken = _userTokenFactory.Create();
                userToken.UserId = user.UserId;
                userToken.Token = refreshToken;
                userToken.ExpiresAt = refreshExpiration;

                await userToken.SaveAsync();
            }
            else
            {
                refreshToken = null;
            }

            var token = new JwtSecurityToken(
                _tokensConfiguration.Issuer,
                _tokensConfiguration.Issuer,
                allClaims,
                expires: expiresAt,
                signingCredentials: creds);

            var userModel = (user != null)
                ? _mapper.Map<AppUserReadOnlyModel>(user)
                : null;

            return Ok(
                new TokenResponseModel
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    refresh_token = refreshToken,
                    token_type = "Bearer",
                    user_name = user.EmailAddress,
                    expiration = expiresAt,
                    expires_in_minutes = _tokensConfiguration.ExpirationInMinutes,
                    user = userModel,
                });
        }

        private List<Claim> GetClaimsFromUser(IAppUserReadOnly user)
        {
            var claims = new List<Claim>();
            AddClaimIfHasValue(claims, ApplicationClaims.Title, user.Title);
            AddClaimIfHasValue(claims, ApplicationClaims.FullName, user.FullName);
            AddClaimIfHasValue(claims, ApplicationClaims.EmailAddress, user.EmailAddress);
            AddClaimIfHasValue(claims, ApplicationClaims.UserId, user.UserId.ToString());

            claims.Add(new Claim(ClaimTypes.Role, ApplicationClaims.User));

            claims.AddRange(user.Claims.Select(r => new Claim(ClaimTypes.Role, r.ClaimName)));

            return claims;
        }

        private void AddClaimIfHasValue(List<Claim> claims, string type, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            claims.Add(new Claim(type, value));
        }
    }
}

