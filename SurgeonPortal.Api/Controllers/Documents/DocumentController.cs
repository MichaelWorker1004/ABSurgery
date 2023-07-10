using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Models.Documents;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Documents
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/documents")]
	public class DocumentController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public DocumentController(
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
        [HttpDelete("")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] IDocumentFactory documentFactory,
            int id)
        {
            var item = await documentFactory.GetByIdAsync(id);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<DocumentModel>> GetDocument_GetByIdAsync(
            [FromServices] IDocumentFactory documentFactory,
            int id)
        {
            var item = await documentFactory.GetByIdAsync(id);
        
            return Ok(_mapper.Map<DocumentModel>(item));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DocumentReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<DocumentReadOnlyModel>>> GetDocumentReadOnly_GetByUserIdAsync(
            [FromServices] IDocumentReadOnlyListFactory documentReadOnlyListFactory)
        {
            var items = await documentReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<DocumentReadOnlyModel>>(items));
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadById([FromServices] IDocumentFactory documentFactory, 
            int id)
        {
            var item = await documentFactory.GetByIdAsync(id);

            // This usage of File() always triggers the browser to perform a file download.
            return File(item.File, "application/pdf", item.DocumentName);
        }

    }
}

