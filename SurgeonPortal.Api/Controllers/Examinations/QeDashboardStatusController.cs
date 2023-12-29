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

namespace SurgeonPortal.Api.Controllers.Examinations
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/examinations/qe-dashboard-status")]
	public class QeDashboardStatusController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public QeDashboardStatusController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QeDashboardStatusReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-examid")]
        public async Task<ActionResult<IEnumerable<QeDashboardStatusReadOnlyModel>>> GetQeDashboardStatusReadOnly_GetByExamIdAsync(
            [FromServices] IQeDashboardStatusReadOnlyListFactory qeDashboardStatusReadOnlyListFactory,
            int examheaderId)
        {
            var items = await qeDashboardStatusReadOnlyListFactory.GetByExamIdAsync(examheaderId);
        
            return Ok(_mapper.Map<IEnumerable<QeDashboardStatusReadOnlyModel>>(items));
        } 
    }
}

