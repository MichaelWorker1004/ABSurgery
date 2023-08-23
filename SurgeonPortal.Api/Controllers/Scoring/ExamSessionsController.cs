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
	[Route("api/exam-sessions")]
	public class ExamSessionsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ExamSessionsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamSessionLockCommandModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("lock")]
        public async Task<IActionResult> LockCommandAsync(
            [FromServices] IExamSessionLockCommandFactory examSessionLockCommandFactory,
            [FromBody] ExamSessionLockCommandModel model)
        {
            if (model == null)
            {
                return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
            }
        
            var command = await examSessionLockCommandFactory.LockExamSessionAsync(model.ExamscheduleId);
        
            return Ok();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamSessionReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-date")]
        public async Task<ActionResult<IEnumerable<ExamSessionReadOnlyModel>>> GetExamSessionReadOnly_GetByUserIdAsync(
            [FromServices] IExamSessionReadOnlyListFactory examSessionReadOnlyListFactory,
            DateTime examDate)
        {
            var items = await examSessionReadOnlyListFactory.GetByUserIdAsync(examDate);
        
            return Ok(_mapper.Map<IEnumerable<ExamSessionReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamSessionSkipCommandModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("skip")]
        public async Task<IActionResult> SkipCommandAsync(
            [FromServices] IExamSessionSkipCommandFactory examSessionSkipCommandFactory,
            [FromBody] ExamSessionSkipCommandModel model)
        {
            if (model == null)
            {
                return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
            }
        
            var command = await examSessionSkipCommandFactory.SkipExamSessionAsync(model.ExamScheduleId);
        
            return Ok();
        } 
    }
}

