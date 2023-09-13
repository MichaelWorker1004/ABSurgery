using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Examiners;
using SurgeonPortal.Models.Examiners;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Examiners
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/examiners/conflict")]
	public class ConflictController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ConflictController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConflictReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-exam-header-id")]
        public async Task<ActionResult<ConflictReadOnlyModel>> GetConflictReadOnly_GetByExamHeaderIdAsync(
            [FromServices] IConflictReadOnlyFactory conflictReadOnlyFactory,
            int examHeaderId)
        {
            var item = await conflictReadOnlyFactory.GetByExamHeaderIdAsync(examHeaderId);
        
            return Ok(_mapper.Map<ConflictReadOnlyModel>(item));
        } 
    }
}

