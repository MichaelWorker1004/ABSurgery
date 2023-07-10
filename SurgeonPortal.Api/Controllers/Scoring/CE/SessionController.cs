using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using SurgeonPortal.Models.Scoring.CE;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Scoring.CE
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/scoring/ce/session")]
	public class SessionController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public SessionController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamineeReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("examinee")]
        public async Task<ActionResult<ExamineeReadOnlyModel>> GetExamineeReadOnly_GetByIdAsync(
            [FromServices] IExamineeReadOnlyFactory examineeReadOnlyFactory,
            int examScheduleId)
        {
            var item = await examineeReadOnlyFactory.GetByIdAsync(examScheduleId);
        
            return Ok(_mapper.Map<ExamineeReadOnlyModel>(item));
        } 
    }
}

