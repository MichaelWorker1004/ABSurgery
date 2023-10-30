using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Models.Scoring;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Scoring
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/cases/feedback")]
	public class CaseFeedbackController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CaseFeedbackController(
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
            [FromServices] ICaseFeedbackFactory caseFeedbackFactory,
            int id)
        {
            var item = await caseFeedbackFactory.GetByIdAsync(id);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
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
            [FromServices] ICaseFeedbackFactory caseFeedbackFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] CaseFeedbackModel model)
        {
            var item = caseFeedbackFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<CaseFeedbackModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/cases/feedback/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(CaseFeedbackModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] ICaseFeedbackFactory caseFeedbackFactory,
            [FromBody] CaseFeedbackModel model,
            int id)
        {
            var item = await caseFeedbackFactory.GetByIdAsync(id);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<CaseFeedbackModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseFeedbackReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-examiner-id")]
        public async Task<ActionResult<CaseFeedbackReadOnlyModel>> GetCaseFeedbackReadOnly_GetByExaminerIdAsync(
            [FromServices] ICaseFeedbackReadOnlyFactory caseFeedbackReadOnlyFactory,
            int caseHeaderId)
        {
            var item = await caseFeedbackReadOnlyFactory.GetByExaminerIdAsync(caseHeaderId);
        
            return Ok(_mapper.Map<CaseFeedbackReadOnlyModel>(item));
        } 

        private void AssignCreateProperties(ICaseFeedback entity, CaseFeedbackModel model)
        {
            entity.UserId = model.UserId;
            entity.CaseHeaderId = model.CaseHeaderId;
            entity.Feedback = model.Feedback;
        }

        private void AssignEditProperties(ICaseFeedback entity, CaseFeedbackModel model)
        {
            entity.CaseHeaderId = model.CaseHeaderId;
            entity.Feedback = model.Feedback;
        }
    }
}

