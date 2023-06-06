using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Models.MedicalTraining;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.MedicalTraining
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/fellowships")]
	public class FellowshipController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public FellowshipController(
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
            [FromServices] IFellowshipFactory fellowshipFactory,
            int id)
        {
            var item = await fellowshipFactory.GetByIdAsync(id);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FellowshipModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<FellowshipModel>> GetFellowship_GetByIdAsync(
            [FromServices] IFellowshipFactory fellowshipFactory,
            int id)
        {
            var item = await fellowshipFactory.GetByIdAsync(id);
        
            return Ok(_mapper.Map<FellowshipModel>(item));
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
            [FromServices] IFellowshipFactory fellowshipFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] FellowshipModel model)
        {
            var item = fellowshipFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<FellowshipModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/fellowships/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(FellowshipModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IFellowshipFactory fellowshipFactory,
            [FromBody] FellowshipModel model,
            int id)
        {
            var item = await fellowshipFactory.GetByIdAsync(id);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<FellowshipModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FellowshipReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<FellowshipReadOnlyModel>>> GetFellowshipReadOnly_GetByUserIdAsync(
            [FromServices] IFellowshipReadOnlyListFactory fellowshipReadOnlyListFactory)
        {
            var items = await fellowshipReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<FellowshipReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IFellowship entity, FellowshipModel model)
        {
            entity.ProgramName = model.ProgramName;
            entity.CompletionYear = model.CompletionYear;
            entity.ProgramOther = model.ProgramOther;
        }

        private void AssignEditProperties(IFellowship entity, FellowshipModel model)
        {
            entity.ProgramName = model.ProgramName;
            entity.CompletionYear = model.CompletionYear;
            entity.ProgramOther = model.ProgramOther;
        }
    }
}

