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
	[Route("api/qualifying-exam")]
	public class QualifyingExamController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public QualifyingExamController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QualifyingExamReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<QualifyingExamReadOnlyModel>> GetQualifyingExamReadOnly_GetAsync(
            [FromServices] IQualifyingExamReadOnlyFactory qualifyingExamReadOnlyFactory)
        {
            var item = await qualifyingExamReadOnlyFactory.GetAsync();
        
            return Ok(_mapper.Map<QualifyingExamReadOnlyModel>(item));
        } 
    }
}

