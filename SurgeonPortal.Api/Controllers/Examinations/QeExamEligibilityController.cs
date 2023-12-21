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
	[Route("api/examinations/qe-exam-eligibility")]
	public class QeExamEligibilityController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public QeExamEligibilityController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QeExamEligibilityReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<QeExamEligibilityReadOnlyModel>>> GetQeExamEligibilityReadOnly_GetByUserIdAsync(
            [FromServices] IQeExamEligibilityReadOnlyListFactory qeExamEligibilityReadOnlyListFactory)
        {
            var items = await qeExamEligibilityReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<QeExamEligibilityReadOnlyModel>>(items));
        } 
    }
}

