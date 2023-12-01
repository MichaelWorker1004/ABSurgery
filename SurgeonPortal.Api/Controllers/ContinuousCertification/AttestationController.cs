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
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.ContinuousCertification
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/continuous-certification/attestation")]
	public class AttestationController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public AttestationController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AttestationReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-user-id")]
        public async Task<ActionResult<IEnumerable<AttestationReadOnlyModel>>> GetAttestationReadOnly_GetAllByUserIdAsync(
            [FromServices] IAttestationReadOnlyListFactory attestationReadOnlyListFactory)
        {
            var items = await attestationReadOnlyListFactory.GetAllByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<AttestationReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubmitAttestationsCommandModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitCommandAsync(
            [FromServices] ISubmitAttestationsCommandFactory submitAttestationsCommandFactory,
            [FromBody] SubmitAttestationsCommandModel model)
        {
            if (model == null)
            {
                return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
            }
        
            var command = await submitAttestationsCommandFactory.GetExamCasesScoredAsync(
                model.SigReceive,
                model.CertnoticeReceive);
        
            return Ok();
        } 
    }
}

