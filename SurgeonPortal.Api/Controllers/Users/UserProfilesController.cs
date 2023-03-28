using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Models.Users;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Users
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
    [Route("user/profiles")]
	[Route("v{version:apiVersion}/user/profiles")]
	public class UserProfilesController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public UserProfilesController(
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
            : base(webHostEnvironment)
        {
            _mapper = mapper;
        }

        [Authorize]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<UserProfileModel>> GetUserProfile_GetByUserIdAsync(
            [FromServices] IUserProfileFactory userProfileFactory,
            int userId)
        {
            var item = await userProfileFactory.GetByUserIdAsync(userId);
        
            return Ok(_mapper.Map<UserProfileModel>(item));
        } 

        [Authorize]
        [MapToApiVersion("1")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("")]
        public async Task<IActionResult> CreateAsync(
            [FromServices] IUserProfileFactory userProfileFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] UserProfileModel model)
        {
            var item = userProfileFactory.Create();
            AssignProperties(item, model);
        
            return await CreateAsync<UserProfileModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/v1/user/profiles/{item.UserProfileId}"));
        } 

        [Authorize]
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(UserProfileModel))]
        [ProducesResponseType(401)]
        [HttpPut("{UserId}")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IUserProfileFactory userProfileFactory,
            [FromBody] UserProfileModel model,
            int userId)
        {
            var item = await userProfileFactory.GetByUserIdAsync(userId);
            AssignProperties(item, model);
            
            return await UpdateAsync<UserProfileModel>(_mapper, item);
        } 

        private void AssignProperties(IUserProfile entity, UserProfileModel model)
        {
            entity.UserId = model.UserId;
            entity.FirstName = model.FirstName;
            entity.MiddleName = model.MiddleName;
            entity.LastName = model.LastName;
            entity.Suffix = model.Suffix;
            entity.DisplayName = model.DisplayName;
            entity.OfficePhoneNumber = model.OfficePhoneNumber;
            entity.MobilePhoneNumber = model.MobilePhoneNumber;
            entity.BirthCity = model.BirthCity;
            entity.BirthState = model.BirthState;
            entity.BirthCountry = model.BirthCountry;
            entity.CountryCitizenship = model.CountryCitizenship;
            entity.ABSId = model.ABSId;
            entity.CertificationStatusId = model.CertificationStatusId;
            entity.NPI = model.NPI;
            entity.Gender = model.Gender;
            entity.BirthDate = model.BirthDate;
            entity.Race = model.Race;
            entity.Ethnicity = model.Ethnicity;
            entity.FirstLanguage = model.FirstLanguage;
            entity.BestLanguage = model.BestLanguage;
            entity.ReceiveComms = model.ReceiveComms;
            entity.UserConfirmed = model.UserConfirmed;
            entity.UserConfirmedDate = model.UserConfirmedDate;
        }
    }
}

