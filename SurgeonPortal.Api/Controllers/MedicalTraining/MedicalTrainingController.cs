using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Models.MedicalTraining;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.MedicalTraining
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/medical-training")]
	public class MedicalTrainingController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public MedicalTrainingController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MedicalTrainingModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<MedicalTrainingModel>> GetMedicalTraining_GetByUserIdAsync(
            [FromServices] IMedicalTrainingFactory medicalTrainingFactory)
        {
            var item = await medicalTrainingFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<MedicalTrainingModel>(item));
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
            [FromServices] IMedicalTrainingFactory medicalTrainingFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] MedicalTrainingModel model)
        {
            var item = medicalTrainingFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<MedicalTrainingModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/medical-training/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(MedicalTrainingModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IMedicalTrainingFactory medicalTrainingFactory,
            [FromBody] MedicalTrainingModel model,
            int userId)
        {
            var item = await medicalTrainingFactory.GetByUserIdAsync();
            AssignEditProperties(item, model);
            
            return await UpdateAsync<MedicalTrainingModel>(_mapper, item);
        } 

        private void AssignCreateProperties(IMedicalTraining entity, MedicalTrainingModel model)
        {
            entity.GraduateProfileId = model.GraduateProfileId;
            entity.MedicalSchoolName = model.MedicalSchoolName;
            entity.MedicalSchoolCity = model.MedicalSchoolCity;
            entity.MedicalSchoolStateId = model.MedicalSchoolStateId;
            entity.MedicalSchoolCountryId = model.MedicalSchoolCountryId;
            entity.DegreeId = model.DegreeId;
            entity.MedicalSchoolCompletionYear = model.MedicalSchoolCompletionYear;
            entity.ResidencyProgramName = model.ResidencyProgramName;
            entity.ResidencyCompletionYear = model.ResidencyCompletionYear;
            entity.ResidencyProgramOther = model.ResidencyProgramOther;
        }

        private void AssignEditProperties(IMedicalTraining entity, MedicalTrainingModel model)
        {
            entity.GraduateProfileId = model.GraduateProfileId;
            entity.MedicalSchoolName = model.MedicalSchoolName;
            entity.MedicalSchoolCity = model.MedicalSchoolCity;
            entity.MedicalSchoolStateId = model.MedicalSchoolStateId;
            entity.MedicalSchoolCountryId = model.MedicalSchoolCountryId;
            entity.DegreeId = model.DegreeId;
            entity.MedicalSchoolCompletionYear = model.MedicalSchoolCompletionYear;
            entity.ResidencyProgramName = model.ResidencyProgramName;
            entity.ResidencyCompletionYear = model.ResidencyCompletionYear;
            entity.ResidencyProgramOther = model.ResidencyProgramOther;
        }
    }
}

