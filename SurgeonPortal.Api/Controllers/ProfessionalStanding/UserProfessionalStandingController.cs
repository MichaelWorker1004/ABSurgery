using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using SurgeonPortal.Models.ProfessionalStanding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.ProfessionalStanding
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/professional-standing")]
	public class UserProfessionalStandingController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public UserProfessionalStandingController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfessionalStandingModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<UserProfessionalStandingModel>> GetUserProfessionalStanding_GetByUserIdAsync(
            [FromServices] IUserProfessionalStandingFactory userProfessionalStandingFactory)
        {
            var item = await userProfessionalStandingFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<UserProfessionalStandingModel>(item));
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
            [FromServices] IUserProfessionalStandingFactory userProfessionalStandingFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] UserProfessionalStandingModel model)
        {
            var item = userProfessionalStandingFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<UserProfessionalStandingModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/professional-standing/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(UserProfessionalStandingModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IUserProfessionalStandingFactory userProfessionalStandingFactory,
            [FromBody] UserProfessionalStandingModel model)
        {
            var item = await userProfessionalStandingFactory.GetByUserIdAsync();
            AssignEditProperties(item, model);
            
            return await UpdateAsync<UserProfessionalStandingModel>(_mapper, item);
        } 

        private void AssignCreateProperties(IUserProfessionalStanding entity, UserProfessionalStandingModel model)
        {
            entity.PrimaryPracticeId = model.PrimaryPracticeId;
            entity.OrganizationTypeId = model.OrganizationTypeId;
            entity.ExplanationOfNonPrivileges = model.ExplanationOfNonPrivileges;
            entity.ExplanationOfNonClinicalActivities = model.ExplanationOfNonClinicalActivities;
        }

        private void AssignEditProperties(IUserProfessionalStanding entity, UserProfessionalStandingModel model)
        {
            entity.PrimaryPracticeId = model.PrimaryPracticeId;
            entity.OrganizationTypeId = model.OrganizationTypeId;
            entity.ExplanationOfNonPrivileges = model.ExplanationOfNonPrivileges;
            entity.ExplanationOfNonClinicalActivities = model.ExplanationOfNonClinicalActivities;
        }
    }
}

