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
	[Route("api/scoring/dashboard")]
	public class DashboardController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public DashboardController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DashboardRosterReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("dashboard-roster")]
        public async Task<ActionResult<IEnumerable<DashboardRosterReadOnlyModel>>> GetDashboardRosterReadOnly_GetByUserIdAsync(
            [FromServices] IDashboardRosterReadOnlyListFactory dashboardRosterReadOnlyListFactory,
            DateTime examDate)
        {
            var items = await dashboardRosterReadOnlyListFactory.GetByUserIdAsync(examDate);
        
            return Ok(_mapper.Map<IEnumerable<DashboardRosterReadOnlyModel>>(items));
        } 
    }
}

