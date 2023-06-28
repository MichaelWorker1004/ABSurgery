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
	[Route("api/exam-headers/cases")]
	public class CasesController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CasesController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaseRosterReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("case-roster")]
        public async Task<ActionResult<IEnumerable<CaseRosterReadOnlyModel>>> GetCaseRosterReadOnly_GetByScheduleIdAsync(
            [FromServices] ICaseRosterReadOnlyListFactory caseRosterReadOnlyListFactory,
            int scheduleId1,
            int scheduleId2)
        {
            var items = await caseRosterReadOnlyListFactory.GetByScheduleIdAsync(
                scheduleId1,
                scheduleId2);
        
            return Ok(_mapper.Map<IEnumerable<CaseRosterReadOnlyModel>>(items));
        } 
    }
}

