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
	[Route("api/billing/application-fee")]
	public class ApplicationFeeController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ApplicationFeeController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationFeeReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-examid")]
        public async Task<ActionResult<ApplicationFeeReadOnlyModel>> GetApplicationFeeReadOnly_GetByExamIdAsync(
            [FromServices] IApplicationFeeReadOnlyFactory applicationFeeReadOnlyFactory,
            int examId)
        {
            var item = await applicationFeeReadOnlyFactory.GetByExamIdAsync(examId);
        
            return Ok(_mapper.Map<ApplicationFeeReadOnlyModel>(item));
        } 
    }
}

