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
using SurgeonPortal.Library.Contracts.Identity;

namespace SurgeonPortal.Api.Controllers.Users
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/users")]
	public class UsersController : YtgControllerBase
	{
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly TokensConfiguration _tokensConfiguration;
        private readonly IUserTokenFactory _userTokenFactory;
        private readonly IIdentityProvider _identityProvider;
        private readonly IUserLoginAuditCommandFactory _userLoginAuditCommandFactory;

        public UsersController(IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IOptions<TokensConfiguration> tokensConfiguration,
            IUserTokenFactory userTokenFactory,
            IIdentityProvider identityProvider,
            IUserLoginAuditCommandFactory userLoginAuditCommandFactory)
            : base(webHostEnvironment)
        {
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<UsersController>();
            _userTokenFactory = userTokenFactory;
            _identityProvider = identityProvider;
            _tokensConfiguration = tokensConfiguration.Value;
            _userLoginAuditCommandFactory = userLoginAuditCommandFactory;
        }

        [AllowAnonymous]
        [MapToApiVersion("1")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("authenticate")]
        public async Task<ActionResult> GetUserToken([FromServices] IAppUserReadOnlyFactory appUserReadOnlyFactory,
            [FromBody] UserLoginModel model)
        {
            var ipAddress = string.Empty;
            var userAgent = string.Empty;

            try
            {
                ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
                userAgent = HttpContext?.Request.Headers["User-Agent"];

                var user = await appUserReadOnlyFactory.GetByCredentialsAsync(
                    model.UserName,
                    model.Password);

                //log successfuly attempt
                await _userLoginAuditCommandFactory.AuditAsync(user.UserId, model.UserName, 1, ipAddress, userAgent, true, string.Empty);
                _logger.LogInformation($"User authenticated successfully. User: {user.UserId}");

                var claims = GetClaimsFromUser(user);

                return await GenerateTokenAsync(user, claims);
            }
            catch (DataPortalException ex) when (ex.BusinessException is AuthenticationFailedException)
            {
                await _userLoginAuditCommandFactory.AuditAsync(-1, model.UserName, 1, ipAddress, userAgent, false, string.Empty);

                _logger.LogInformation($"GetUserToken(): User failed authentication. ({model.UserName})");
                return BadRequest("Login failed");
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
            if (User.Identity != null)
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

		[MapToApiVersion("1")]
		[ApiExplorerSettings(IgnoreApi = true)]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserReadOnlyModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetpasswordCommandAsync(
			[FromServices] IPasswordResetCommandFactory passwordResetCommandFactory,
			[FromBody] PasswordResetCommandModel model)
		{
			if (model == null)
			{
				return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
			}

			var command = await passwordResetCommandFactory.ResetPasswordAsync(
				model.OldPassword,
				model.NewPassword);

            if(command.PasswordReset.HasValue && command.PasswordReset.Value)
            {
				return Ok();
			}
            else
            {
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
                    user_name = user?.UserName ?? "Unknown",
                    expiration = expiresAt,
                    expires_in_minutes = _tokensConfiguration.ExpirationInMinutes,
                    user = userModel,
                });
        }

        private List<Claim> GetClaimsFromUser(IAppUserReadOnly user)
        {
            var claims = new List<Claim>();
            AddClaimIfHasValue(claims, YtgClaimType.FullName, user.FullName);
            AddClaimIfHasValue(claims, YtgClaimType.EmailAddress, user.EmailAddress);
            AddClaimIfHasValue(claims, YtgClaimType.UserId, user.UserId.ToString());

            var translatedClaims = user.Claims.Select(c => MapRoleClaimNames(c.ClaimName))
                .Where(c => !string.IsNullOrEmpty(c))
                .Select(c => c!);

            claims.AddRange(translatedClaims.Select(r => new Claim(ClaimTypes.Role, r)));

            CheckUserHasMinimumClaims(claims);

            return claims;
        }
        private void CheckUserHasMinimumClaims(List<Claim> claims)
        {
            var message = "The user is missing the minimum claims to login to the Surgeon Portal";

            if (!UserHasClaim(claims, SurgeonPortalClaims.TraineeClaim) &&
                !UserHasClaim(claims, SurgeonPortalClaims.SurgeonClaim) &&
                !UserHasClaim(claims, SurgeonPortalClaims.ExaminerClaim))
            {
                throw new AuthenticationFailedException(message);
            }

            if (!UserHasClaim(claims, SurgeonPortalClaims.UserClaim))
            {
                throw new AuthenticationFailedException(message);
            }

        }
        private bool UserHasClaim(List<Claim> userClaims,
            string claimName)
        {
            return userClaims.Any(claim => claim.Type == ClaimTypes.Role && claim.Value == claimName);
        }

        private void AddClaimIfHasValue(List<Claim> claims, string type, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            claims.Add(new Claim(type, value));
        }

        private string? MapRoleClaimNames(string claimName) => claimName switch
        {
			ApplicationClaims.User => SurgeonPortalClaims.UserClaim,
            ApplicationClaims.Trainee => SurgeonPortalClaims.TraineeClaim,
            ApplicationClaims.Surgeon => SurgeonPortalClaims.SurgeonClaim,
            ApplicationClaims.Examiner => SurgeonPortalClaims.ExaminerClaim,
            _ => null
        };
    }
}

