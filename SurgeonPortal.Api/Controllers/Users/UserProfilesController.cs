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
  [Route("api/users/profiles")]
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

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userId")]
        public async Task<ActionResult<UserProfileModel>> GetUserProfile_GetByUserIdAsync(
            [FromServices] IUserProfileFactory userProfileFactory,
            int userId)
        {
            var item = await userProfileFactory.GetByUserIdAsync(userId);
        
            return Ok(_mapper.Map<UserProfileModel>(item));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
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
            AssignCreateProperties(item, model);
        
            return await CreateAsync<UserProfileModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/v1/users/profiles/{item.UserProfileId}"));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(UserProfileModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IUserProfileFactory userProfileFactory,
            [FromBody] UserProfileModel model,
            int userId)
        {
            var item = await userProfileFactory.GetByUserIdAsync(userId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<UserProfileModel>(_mapper, item);
        } 

    private void AssignCreateProperties(IUserProfile entity, UserProfileModel model)
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
      entity.GenderId = model.GenderId;
      entity.BirthDate = model.BirthDate;
      entity.Race = model.Race;
      entity.Ethnicity = model.Ethnicity;
      entity.FirstLanguageId = model.FirstLanguageId;
      entity.BestLanguageId = model.BestLanguageId;
      entity.ReceiveComms = model.ReceiveComms;
      entity.UserConfirmed = model.UserConfirmed;
      entity.UserConfirmedDate = model.UserConfirmedDate;
      entity.Street1 = model.Street1;
      entity.Street2 = model.Street2;
      entity.City = model.City;
      entity.State = model.State;
      entity.ZipCode = model.ZipCode;
      entity.Country = model.Country;
    }

    private void AssignEditProperties(IUserProfile entity, UserProfileModel model)
    {
      entity.UserProfileId = model.UserProfileId;
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
      entity.GenderId = model.GenderId;
      entity.BirthDate = model.BirthDate;
      entity.Race = model.Race;
      entity.Ethnicity = model.Ethnicity;
      entity.FirstLanguageId = model.FirstLanguageId;
      entity.BestLanguageId = model.BestLanguageId;
      entity.ReceiveComms = model.ReceiveComms;
      entity.UserConfirmed = model.UserConfirmed;
      entity.UserConfirmedDate = model.UserConfirmedDate;
      entity.Street1 = model.Street1;
      entity.Street2 = model.Street2;
      entity.City = model.City;
      entity.State = model.State;
      entity.ZipCode = model.ZipCode;
      entity.Country = model.Country;
    }
  }
}

