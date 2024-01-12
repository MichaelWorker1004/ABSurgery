using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using SurgeonPortal.Models.ContinuousCertification;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.ContinuousCertification
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/continuous-certification/requirements")]
	public class RequirementsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public RequirementsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequirementsReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<RequirementsReadOnlyModel>> GetRequirementsReadOnly_GetByUserIdAsync(
            [FromServices] IRequirementsReadOnlyFactory requirementsReadOnlyFactory,
            int userId)
        {
            var item = await requirementsReadOnlyFactory.GetByUserIdAsync(userId);
        
            return Ok(_mapper.Map<RequirementsReadOnlyModel>(item));
        } 
    }
}

