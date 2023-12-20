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
	[Route("api/examinations/pd-reference-letter")]
	public class PdReferenceLetterController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public PdReferenceLetterController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PdReferenceLetterModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-examid")]
        public async Task<ActionResult<PdReferenceLetterModel>> GetPdReferenceLetter_GetByExamIdAsync(
            [FromServices] IPdReferenceLetterFactory pdReferenceLetterFactory,
            int examId)
        {
            var item = await pdReferenceLetterFactory.GetByExamIdAsync(examId);
        
            return Ok(_mapper.Map<PdReferenceLetterModel>(item));
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
            [FromServices] IPdReferenceLetterFactory pdReferenceLetterFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] PdReferenceLetterModel model)
        {
            var item = pdReferenceLetterFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<PdReferenceLetterModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/examinations/pd-reference-letter/{item.Id}"));
        } 

        private void AssignCreateProperties(IPdReferenceLetter entity, PdReferenceLetterModel model)
        {
            entity.Official = model.Official;
            entity.Title = model.Title;
            entity.Email = model.Email;
            entity.Hosp = model.Hosp;
            entity.ExamId = model.ExamId;
            entity.FullName = model.FullName;
        }
    }
}

