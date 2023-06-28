using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Models.Scoring;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Scoring
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/exam-headers/cases/case-contents/case-comments")]
	public class CaseNotesController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CaseNotesController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseCommentModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<CaseCommentModel>> GetCaseComment_GetByIdAsync(
            [FromServices] ICaseCommentFactory caseCommentFactory,
            int id)
        {
            var item = await caseCommentFactory.GetByIdAsync(id);
        
            return Ok(_mapper.Map<CaseCommentModel>(item));
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
            [FromServices] ICaseCommentFactory caseCommentFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] CaseCommentModel model)
        {
            var item = caseCommentFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<CaseCommentModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/exam-headers/cases/case-contents/case-comments/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(CaseCommentModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] ICaseCommentFactory caseCommentFactory,
            [FromBody] CaseCommentModel model,
            int id)
        {
            var item = await caseCommentFactory.GetByIdAsync(id);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<CaseCommentModel>(_mapper, item);
        } 

        private void AssignCreateProperties(ICaseComment entity, CaseCommentModel model)
        {
            entity.CaseContentId = model.CaseContentId;
            entity.Comments = model.Comments;
        }

        private void AssignEditProperties(ICaseComment entity, CaseCommentModel model)
        {
            entity.CaseContentId = model.CaseContentId;
            entity.Comments = model.Comments;
        }
    }
}

