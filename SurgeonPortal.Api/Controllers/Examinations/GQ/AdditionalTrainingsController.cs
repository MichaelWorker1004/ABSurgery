using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using SurgeonPortal.Models.Examinations.GQ;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Examinations.GQ
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/examinations/gq/additional-trainings")]
	public class AdditionalTrainingsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public AdditionalTrainingsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdditionalTrainingModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<AdditionalTrainingModel>> GetAdditionalTraining_GetByTrainingIdAsync(
            [FromServices] IAdditionalTrainingFactory additionalTrainingFactory,
            int trainingId)
        {
            var item = await additionalTrainingFactory.GetByTrainingIdAsync(trainingId);
        
            return Ok(_mapper.Map<AdditionalTrainingModel>(item));
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
            [FromServices] IAdditionalTrainingFactory additionalTrainingFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] AdditionalTrainingModel model)
        {
            var item = additionalTrainingFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<AdditionalTrainingModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/examinations/gq/additional-trainings/{item.TrainingId}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(AdditionalTrainingModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IAdditionalTrainingFactory additionalTrainingFactory,
            [FromBody] AdditionalTrainingModel model,
            int trainingId)
        {
            var item = await additionalTrainingFactory.GetByTrainingIdAsync(trainingId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<AdditionalTrainingModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdditionalTrainingReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<AdditionalTrainingReadOnlyModel>>> GetAdditionalTrainingReadOnly_GetAllByUserIdAsync(
            [FromServices] IAdditionalTrainingReadOnlyListFactory additionalTrainingReadOnlyListFactory)
        {
            var items = await additionalTrainingReadOnlyListFactory.GetAllByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<AdditionalTrainingReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IAdditionalTraining entity, AdditionalTrainingModel model)
        {
            entity.DateEnded = model.DateEnded;
            entity.DateStarted = model.DateStarted;
            entity.Other = model.Other;
            entity.InstitutionId = model.InstitutionId;
            entity.City = model.City;
            entity.StateId = model.StateId;
            entity.TypeOfTraining = model.TypeOfTraining;
        }

        private void AssignEditProperties(IAdditionalTraining entity, AdditionalTrainingModel model)
        {
            entity.DateEnded = model.DateEnded;
            entity.DateStarted = model.DateStarted;
            entity.Other = model.Other;
            entity.InstitutionId = model.InstitutionId;
            entity.City = model.City;
            entity.StateId = model.StateId;
            entity.TypeOfTraining = model.TypeOfTraining;
        }
    }
}

