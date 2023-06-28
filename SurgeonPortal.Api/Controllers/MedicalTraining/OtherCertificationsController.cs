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
	[Route("api/medical-training/other-certifications")]
	public class OtherCertificationsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public OtherCertificationsController(
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
            [FromServices] IOtherCertificationsFactory otherCertificationsFactory,
            int id)
        {
            var item = await otherCertificationsFactory.GetByIdAsync(id);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OtherCertificationsModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<OtherCertificationsModel>> GetOtherCertifications_GetByIdAsync(
            [FromServices] IOtherCertificationsFactory otherCertificationsFactory,
            int id)
        {
            var item = await otherCertificationsFactory.GetByIdAsync(id);
        
            return Ok(_mapper.Map<OtherCertificationsModel>(item));
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
            [FromServices] IOtherCertificationsFactory otherCertificationsFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] OtherCertificationsModel model)
        {
            var item = otherCertificationsFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<OtherCertificationsModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/medical-training/other-certifications/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(OtherCertificationsModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IOtherCertificationsFactory otherCertificationsFactory,
            [FromBody] OtherCertificationsModel model,
            int id)
        {
            var item = await otherCertificationsFactory.GetByIdAsync(id);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<OtherCertificationsModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OtherCertificationsReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<OtherCertificationsReadOnlyModel>>> GetOtherCertificationsReadOnly_GetByUserIdAsync(
            [FromServices] IOtherCertificationsReadOnlyListFactory otherCertificationsReadOnlyListFactory)
        {
            var items = await otherCertificationsReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<OtherCertificationsReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IOtherCertifications entity, OtherCertificationsModel model)
        {
            entity.CertificateTypeId = model.CertificateTypeId;
            entity.IssueDate = model.IssueDate;
            entity.CertificateNumber = model.CertificateNumber;
        }

        private void AssignEditProperties(IOtherCertifications entity, OtherCertificationsModel model)
        {
            entity.CertificateTypeId = model.CertificateTypeId;
            entity.IssueDate = model.IssueDate;
            entity.CertificateNumber = model.CertificateNumber;
        }
    }
}

