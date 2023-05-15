using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Models.MedicalTraining;
using SurgeonPortal.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Api.Controllers.MedicalTraining
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/advanced-training")]
	public class AdvancedTrainingController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public AdvancedTrainingController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdvancedTrainingModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-trainingid")]
        public async Task<ActionResult<AdvancedTrainingModel>> GetAdvancedTraining_GetByTrainingIdAsync(
            [FromServices] IAdvancedTrainingFactory advancedTrainingFactory,
            int id)
        {
            var item = await advancedTrainingFactory.GetByTrainingIdAsync(id);
        
            return Ok(_mapper.Map<AdvancedTrainingModel>(item));
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
            [FromServices] IAdvancedTrainingFactory advancedTrainingFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] AdvancedTrainingModel model)
        {
            var item = advancedTrainingFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<AdvancedTrainingModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/advanced-training/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(AdvancedTrainingModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IAdvancedTrainingFactory advancedTrainingFactory,
            [FromBody] AdvancedTrainingModel model,
            int id)
        {
            var item = await advancedTrainingFactory.GetByTrainingIdAsync(id);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<AdvancedTrainingModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdvancedTrainingReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<AdvancedTrainingReadOnlyModel>>> GetAdvancedTrainingReadOnly_GetByUserIdAsync(
            [FromServices] IAdvancedTrainingReadOnlyListFactory advancedTrainingReadOnlyListFactory)
        {
            var items = await advancedTrainingReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<AdvancedTrainingReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IAdvancedTraining entity, AdvancedTrainingModel model)
        {
            entity.UserId = IdentityHelper.UserId;
            entity.TrainingTypeId = model.TrainingTypeId;
            entity.ProgramId = model.ProgramId;
            entity.Other = model.Other;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
        }

        private void AssignEditProperties(IAdvancedTraining entity, AdvancedTrainingModel model)
        {
            entity.Id = model.Id;
            entity.UserId = IdentityHelper.UserId;
            entity.TrainingTypeId = model.TrainingTypeId;
            entity.ProgramId = model.ProgramId;
            entity.Other = model.Other;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
        }
    }
}

