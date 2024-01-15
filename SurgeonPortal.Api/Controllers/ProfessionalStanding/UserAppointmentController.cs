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
	[Route("api/professional-standing/user-appointment")]
	public class UserAppointmentController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public UserAppointmentController(
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
        [HttpDelete("")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] IUserAppointmentFactory userAppointmentFactory,
            int apptId)
        {
            var item = await userAppointmentFactory.GetByIdAsync(apptId);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAppointmentModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<UserAppointmentModel>> GetUserAppointment_GetByIdAsync(
            [FromServices] IUserAppointmentFactory userAppointmentFactory,
            int apptId)
        {
            var item = await userAppointmentFactory.GetByIdAsync(apptId);
        
            return Ok(_mapper.Map<UserAppointmentModel>(item));
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
            [FromServices] IUserAppointmentFactory userAppointmentFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] UserAppointmentModel model)
        {
            var item = userAppointmentFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<UserAppointmentModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/professional-standing/user-appointment/{item.ApptId}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(UserAppointmentModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IUserAppointmentFactory userAppointmentFactory,
            [FromBody] UserAppointmentModel model,
            int apptId)
        {
            var item = await userAppointmentFactory.GetByIdAsync(apptId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<UserAppointmentModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserAppointmentReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<UserAppointmentReadOnlyModel>>> GetUserAppointmentReadOnly_GetByUserIdAsync(
            [FromServices] IUserAppointmentReadOnlyListFactory userAppointmentReadOnlyListFactory)
        {
            var items = await userAppointmentReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<UserAppointmentReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IUserAppointment entity, UserAppointmentModel model)
        {
            entity.PracticeTypeId = model.PracticeTypeId;
            entity.AppointmentTypeId = model.AppointmentTypeId;
            entity.OrganizationTypeId = model.OrganizationTypeId;
            entity.StateCode = model.StateCode;
            entity.OrganizationId = model.OrganizationId;
            entity.PrimaryAppointment = model.PrimaryAppointment;
            entity.AuthorizingOfficial = model.AuthorizingOfficial;
            entity.Other = model.Other;
        }

        private void AssignEditProperties(IUserAppointment entity, UserAppointmentModel model)
        {
            entity.PracticeTypeId = model.PracticeTypeId;
            entity.AppointmentTypeId = model.AppointmentTypeId;
            entity.OrganizationTypeId = model.OrganizationTypeId;
            entity.PrimaryAppointment = model.PrimaryAppointment;
            entity.StateCode = model.StateCode;
            entity.OrganizationId = model.OrganizationId;
            entity.AuthorizingOfficial = model.AuthorizingOfficial;
            entity.Other = model.Other;
        }
    }
}

