using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Billing;
using SurgeonPortal.Models.Billing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Billing
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/billing/exam-fee")]
	public class ExamFeeController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ExamFeeController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamFeeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<ExamFeeReadOnlyModel>>> GetExamFeeReadOnly_GetByUserIdAsync(
            [FromServices] IExamFeeReadOnlyListFactory examFeeReadOnlyListFactory)
        {
            var items = await examFeeReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<ExamFeeReadOnlyModel>>(items));
        } 
    }
}

