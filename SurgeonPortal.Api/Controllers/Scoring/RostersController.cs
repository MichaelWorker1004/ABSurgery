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
	[Route("api/rosters")]
	public class RostersController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public RostersController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RosterReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("roster-schedule")]
        public async Task<ActionResult<IEnumerable<RosterReadOnlyModel>>> GetRosterReadOnly_GetByExaminationHeaderIdAsync(
            [FromServices] IRosterReadOnlyListFactory rosterReadOnlyListFactory,
            int examHeaderId)
        {
            var items = await rosterReadOnlyListFactory.GetByExaminationHeaderIdAsync(examHeaderId);
        
            return Ok(_mapper.Map<IEnumerable<RosterReadOnlyModel>>(items));
        } 
    }
}

