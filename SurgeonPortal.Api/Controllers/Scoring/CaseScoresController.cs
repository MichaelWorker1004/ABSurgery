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
	[Route("api/cases/scores")]
	public class CaseScoresController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CaseScoresController(
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
            [FromServices] ICaseScoreFactory caseScoreFactory,
            int examScoringId)
        {
            var item = await caseScoreFactory.GetByIdAsync(examScoringId);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseScoreModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<CaseScoreModel>> GetCaseScore_GetByIdAsync(
            [FromServices] ICaseScoreFactory caseScoreFactory,
            int examScoringId)
        {
            var item = await caseScoreFactory.GetByIdAsync(examScoringId);
        
            return Ok(_mapper.Map<CaseScoreModel>(item));
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
            [FromServices] ICaseScoreFactory caseScoreFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] CaseScoreModel model)
        {
            var item = caseScoreFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<CaseScoreModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/cases/scores/{item.ExamScoringId}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(CaseScoreModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] ICaseScoreFactory caseScoreFactory,
            [FromBody] CaseScoreModel model,
            int examScoringId)
        {
            var item = await caseScoreFactory.GetByIdAsync(examScoringId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<CaseScoreModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaseScoreReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("all-by-examschedule-id")]
        public async Task<ActionResult<IEnumerable<CaseScoreReadOnlyModel>>> GetCaseScoreReadOnly_GetByExamScheduleIdAsync(
            [FromServices] ICaseScoreReadOnlyListFactory caseScoreReadOnlyListFactory,
            int examScheduleId)
        {
            var items = await caseScoreReadOnlyListFactory.GetByExamScheduleIdAsync(examScheduleId);
        
            return Ok(_mapper.Map<IEnumerable<CaseScoreReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(ICaseScore entity, CaseScoreModel model)
        {
            entity.ExamineeUserId = model.ExamineeUserId;
            entity.ExamCaseId = model.ExamCaseId;
            entity.Score = model.Score;
            entity.Remarks = model.Remarks;
            entity.CriticalFail = model.CriticalFail;
        }

        private void AssignEditProperties(ICaseScore entity, CaseScoreModel model)
        {
            entity.ExamineeUserId = model.ExamineeUserId;
            entity.ExamCaseId = model.ExamCaseId;
            entity.Score = model.Score;
            entity.Remarks = model.Remarks;
            entity.CriticalFail = model.CriticalFail;
        }
    }
}

