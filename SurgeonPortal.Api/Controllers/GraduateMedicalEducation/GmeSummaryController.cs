using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Models.GraduateMedicalEducation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.GraduateMedicalEducation
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/gme-summary")]
	public class GmeSummaryController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public GmeSummaryController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GmeSummaryReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<GmeSummaryReadOnlyModel>>> GetGmeSummaryReadOnly_GetByUserIdAsync(
            [FromServices] IGmeSummaryReadOnlyListFactory gmeSummaryReadOnlyListFactory)
        {
            var items = await gmeSummaryReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<GmeSummaryReadOnlyModel>>(items));
        } 
    }
}

