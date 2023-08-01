using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Trainees;
using SurgeonPortal.Models.Trainees;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Trainees
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/trainees/exams")]
	public class ExamController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ExamController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationStatusReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("registration-status")]
        public async Task<ActionResult<RegistrationStatusReadOnlyModel>> GetRegistrationStatusReadOnly_GetByExamCodeAsync(
            [FromServices] IRegistrationStatusReadOnlyFactory registrationStatusReadOnlyFactory,
            string examCode)
        {
            var item = await registrationStatusReadOnlyFactory.GetByExamCodeAsync(examCode);
        
            return Ok(_mapper.Map<RegistrationStatusReadOnlyModel>(item));
        } 
    }
}

