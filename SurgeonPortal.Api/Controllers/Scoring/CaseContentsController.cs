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
	[Route("api/exam-headers/cases/{caseHeaderId}/case-contents")]
	public class CaseContentsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CaseContentsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaseDetailReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("detail")]
        public async Task<ActionResult<IEnumerable<CaseDetailReadOnlyModel>>> GetCaseDetailReadOnly_GetByCaseHeaderIdAsync(
            [FromServices] ICaseDetailReadOnlyListFactory caseDetailReadOnlyListFactory,
            int caseHeaderId)
        {
            var items = await caseDetailReadOnlyListFactory.GetByCaseHeaderIdAsync(caseHeaderId);
        
            return Ok(_mapper.Map<IEnumerable<CaseDetailReadOnlyModel>>(items));
        } 
    }
}

