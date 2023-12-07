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
	[Route("api/exam/accommodations")]
	public class AccommodationController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public AccommodationController(
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
            [FromServices] IAccommodationFactory accommodationFactory,
            int examId)
        {
            var item = await accommodationFactory.GetByExamIdAsync(examId);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccommodationModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-exam-id")]
        public async Task<ActionResult<AccommodationModel>> GetAccommodation_GetByExamIdAsync(
            [FromServices] IAccommodationFactory accommodationFactory,
            int examId)
        {
            var item = await accommodationFactory.GetByExamIdAsync(examId);
        
            return Ok(_mapper.Map<AccommodationModel>(item));
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
            [FromServices] IAccommodationFactory accommodationFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromForm] AccommodationModel model)
        {
            var item = accommodationFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<AccommodationModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/exam/accommodations/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(AccommodationModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IAccommodationFactory accommodationFactory,
            [FromForm] AccommodationModel model,
            int examId)
        {
            var item = await accommodationFactory.GetByExamIdAsync(examId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<AccommodationModel>(_mapper, item);
        } 

        private void AssignCreateProperties(IAccommodation entity, AccommodationModel model)
        {
            entity.AccommodationID = model.AccommodationID;
            entity.DocumentId = model.DocumentId;
            entity.ExamID = model.ExamID;
			LoadDocument(entity, model);
		}

        private void AssignEditProperties(IAccommodation entity, AccommodationModel model)
        {
            entity.AccommodationID = model.AccommodationID;
            entity.DocumentId = model.DocumentId;
            entity.ExamID = model.ExamID;
            LoadDocument(entity, model);
        }

        private async void LoadDocument(IAccommodation entity, AccommodationModel model)
        {
            if(model.File != null)
            {
                using (var stream = new MemoryStream())
                {
                    await model.File.CopyToAsync(stream);
                    var fileBytes = stream.ToArray();

                    entity.LoadDocument(new MemoryStream(fileBytes));
                }
            }
        }
    }
}

