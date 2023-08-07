using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Dev;
using SurgeonPortal.Models.Dev;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Dev
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/dev/reset")]
    [ApiExplorerSettings(IgnoreApi = true)]
	public class ResetDataController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ResetDataController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResetCaseCommentsCommandModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("case-comments")]
        public async Task<IActionResult> CasecommentsCommandAsync(
            [FromServices] IResetCaseCommentsCommandFactory resetCaseCommentsCommandFactory,
            [FromBody] ResetCaseCommentsCommandModel model)
        {
            if (model == null)
            {
                return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
            }
        
            var command = await resetCaseCommentsCommandFactory.ResetCaseCommentsAsync();
        
            return Ok();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResetScoresCommandModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("exam-scores")]
        public async Task<IActionResult> ExamscoresCommandAsync(
            [FromServices] IResetScoresCommandFactory resetScoresCommandFactory,
            [FromBody] ResetScoresCommandModel model)
        {
            if (model == null)
            {
                return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
            }
        
            var command = await resetScoresCommandFactory.ResetExamScoresAsync();
        
            return Ok();
        } 
    }
}

