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
	[Route("api/continuous-certification/reference-letter")]
	public class ReferenceLetterController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ReferenceLetterController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReferenceLetterModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<ReferenceLetterModel>> GetReferenceLetter_GetByIdAsync(
            [FromServices] IReferenceLetterFactory referenceLetterFactory,
            int id)
        {
            var item = await referenceLetterFactory.GetByIdAsync(id);
        
            return Ok(_mapper.Map<ReferenceLetterModel>(item));
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
            [FromServices] IReferenceLetterFactory referenceLetterFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] ReferenceLetterModel model)
        {
            var item = referenceLetterFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<ReferenceLetterModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/continuous-certification/reference-letter/{item.RoleId}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReferenceLetterReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("all-by-user-id")]
        public async Task<ActionResult<IEnumerable<ReferenceLetterReadOnlyModel>>> GetReferenceLetterReadOnly_GetAllByUserIdAsync(
            [FromServices] IReferenceLetterReadOnlyListFactory referenceLetterReadOnlyListFactory)
        {
            var items = await referenceLetterReadOnlyListFactory.GetAllByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<ReferenceLetterReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IReferenceLetter entity, ReferenceLetterModel model)
        {
            entity.Official = model.Official;
            entity.Title = model.Title;
            entity.RoleId = model.RoleId;
            entity.AltRoleId = model.AltRoleId;
            entity.Explain = model.Explain;
            entity.Email = model.Email;
            entity.Phone = model.Phone;
            entity.Hosp = model.Hosp;
            entity.City = model.City;
            entity.State = model.State;
            entity.SecOrder = model.SecOrder;
        }
    }
}

