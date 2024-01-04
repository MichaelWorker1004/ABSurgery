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
	[Route("api/examinations/intentions")]
	public class ExamIntentionsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ExamIntentionsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamIntentionsModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-examid")]
        public async Task<ActionResult<ExamIntentionsModel>> GetExamIntentions_GetByExamIdAsync(
            [FromServices] IExamIntentionsFactory examIntentionsFactory,
            int examId)
        {
            var item = await examIntentionsFactory.GetByExamIdAsync(examId);
        
            return Ok(_mapper.Map<ExamIntentionsModel>(item));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("")]
        public async Task<IActionResult> CreateAsync(
            [FromServices] IExamIntentionsFactory examIntentionsFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] ExamIntentionsModel model)
        {
            var item = examIntentionsFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<ExamIntentionsModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/examinations/intentions/"));
        } 

        private void AssignCreateProperties(IExamIntentions entity, ExamIntentionsModel model)
        {
            entity.ExamId = model.ExamId;
            entity.Intention = model.Intention;
        }
    }
}

