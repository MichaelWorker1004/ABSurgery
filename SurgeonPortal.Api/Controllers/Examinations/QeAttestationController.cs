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
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Examinations
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/examinations/qe-attestation")]
	public class QeAttestationController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public QeAttestationController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QeAttestationReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-examid")]
        public async Task<ActionResult<IEnumerable<QeAttestationReadOnlyModel>>> GetQeAttestationReadOnly_GetByExamIdAsync(
            [FromServices] IQeAttestationReadOnlyListFactory qeAttestationReadOnlyListFactory,
            int examId)
        {
            var items = await qeAttestationReadOnlyListFactory.GetByExamIdAsync(examId);
        
            return Ok(_mapper.Map<IEnumerable<QeAttestationReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateQeAttestationsCommandModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateCommandAsync(
            [FromServices] IUpdateQeAttestationsCommandFactory updateQeAttestationsCommandFactory,
            [FromBody] UpdateQeAttestationsCommandModel model)
        {
            if (model == null)
            {
                return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
            }
        
            var command = await updateQeAttestationsCommandFactory.UpdateQeAttestationsAsync(model.ExamId);
        
            return Ok();
        } 
    }
}

