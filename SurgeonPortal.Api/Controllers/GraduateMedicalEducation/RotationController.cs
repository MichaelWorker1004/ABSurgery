using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Models.GraduateMedicalEducation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.GraduateMedicalEducation
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/graduate-medical-education")]
	public class RotationController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public RotationController(
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
            [FromServices] IRotationFactory rotationFactory,
            int id)
        {
            var item = await rotationFactory.GetByIdAsync(id);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RotationModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<RotationModel>> GetRotation_GetByIdAsync(
            [FromServices] IRotationFactory rotationFactory,
            int id)
        {
            var item = await rotationFactory.GetByIdAsync(id);
        
            return Ok(_mapper.Map<RotationModel>(item));
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
            [FromServices] IRotationFactory rotationFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] RotationModel model)
        {
            var item = rotationFactory.Create();
            AssignCreateProperties(item, model);

            return await CreateAsync<RotationModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/graduate-medical-education/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(RotationModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IRotationFactory rotationFactory,
            [FromBody] RotationModel model,
            int id)
        {
            var item = await rotationFactory.GetByIdAsync(id);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<RotationModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RotationGapReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("gaps")]
        public async Task<ActionResult<IEnumerable<RotationGapReadOnlyModel>>> GetRotationGapReadOnly_GetByUserIdAsync(
            [FromServices] IRotationGapReadOnlyListFactory rotationGapReadOnlyListFactory)
        {
            var items = await rotationGapReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<RotationGapReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RotationReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<RotationReadOnlyModel>>> GetRotationReadOnly_GetByUserIdAsync(
            [FromServices] IRotationReadOnlyListFactory rotationReadOnlyListFactory)
        {
            var items = await rotationReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<RotationReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IRotation entity, RotationModel model)
        {
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.ClinicalLevelId = model.ClinicalLevelId;
            entity.ClinicalActivityId = model.ClinicalActivityId;
            entity.ProgramName = model.ProgramName;
            entity.NonSurgicalActivity = model.NonSurgicalActivity;
            entity.AlternateInstitutionName = model.AlternateInstitutionName;
            entity.IsInternationalRotation = model.IsInternationalRotation;
            entity.Other = model.Other;
        }

        private void AssignEditProperties(IRotation entity, RotationModel model)
        {
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.ClinicalLevelId = model.ClinicalLevelId;
            entity.ClinicalActivityId = model.ClinicalActivityId;
            entity.ProgramName = model.ProgramName;
            entity.NonSurgicalActivity = model.NonSurgicalActivity;
            entity.AlternateInstitutionName = model.AlternateInstitutionName;
            entity.IsInternationalRotation = model.IsInternationalRotation;
            entity.Other = model.Other;
        }
    }
}

