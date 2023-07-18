using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using SurgeonPortal.Models.ContinuingMedicalEducation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.ContinuingMedicalEducation
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/cme")]
	public class CmeController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CmeController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CmeAdjustmentReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("adjustments")]
        public async Task<ActionResult<IEnumerable<CmeAdjustmentReadOnlyModel>>> GetCmeAdjustmentReadOnly_GetByUserIdAsync(
            [FromServices] ICmeAdjustmentReadOnlyListFactory cmeAdjustmentReadOnlyListFactory)
        {
            var items = await cmeAdjustmentReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<CmeAdjustmentReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CmeCreditReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<CmeCreditReadOnlyModel>> GetCmeCreditReadOnly_GetByIdAsync(
            [FromServices] ICmeCreditReadOnlyFactory cmeCreditReadOnlyFactory,
            int cmeId)
        {
            var item = await cmeCreditReadOnlyFactory.GetByIdAsync(cmeId);
        
            return Ok(_mapper.Map<CmeCreditReadOnlyModel>(item));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CmeCreditReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("itemized-cme")]
        public async Task<ActionResult<IEnumerable<CmeCreditReadOnlyModel>>> GetCmeCreditReadOnly_GetByUserIdAsync(
            [FromServices] ICmeCreditReadOnlyListFactory cmeCreditReadOnlyListFactory)
        {
            var items = await cmeCreditReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<CmeCreditReadOnlyModel>>(items));
        } 
    }
}

