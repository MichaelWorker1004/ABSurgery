using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Models.Examinations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Examinations
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/examinations")]
	public class ExaminationsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ExaminationsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamHistoryReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<ExamHistoryReadOnlyModel>>> GetExamHistoryReadOnly_GetByUserIdAsync(
            [FromServices] IExamHistoryReadOnlyListFactory examHistoryReadOnlyListFactory)
        {
            var items = await examHistoryReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<ExamHistoryReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamOverviewReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("overview")]
        public async Task<ActionResult<IEnumerable<ExamOverviewReadOnlyModel>>> GetExamOverviewReadOnly_GetAllAsync(
            [FromServices] IExamOverviewReadOnlyListFactory examOverviewReadOnlyListFactory)
        {
            var items = await examOverviewReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<ExamOverviewReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamTitleReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("title")]
        public async Task<ActionResult<ExamTitleReadOnlyModel>> GetExamTitleReadOnly_GetByExamIdAsync(
            [FromServices] IExamTitleReadOnlyFactory examTitleReadOnlyFactory,
            int examId)
        {
            var item = await examTitleReadOnlyFactory.GetByExamIdAsync(examId);
        
            return Ok(_mapper.Map<ExamTitleReadOnlyModel>(item));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InsertUserExamCommandModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertCommandAsync(
            [FromServices] IInsertUserExamCommandFactory insertUserExamCommandFactory,
            [FromBody] InsertUserExamCommandModel model)
        {
            if (model == null)
            {
                return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
            }
        
            var command = await insertUserExamCommandFactory.InsertUserExamAsync(model.ExamHeaderId);
        
            return Ok();
        } 
    }
}

