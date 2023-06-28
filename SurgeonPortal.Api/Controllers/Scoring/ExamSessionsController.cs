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
    }
}

