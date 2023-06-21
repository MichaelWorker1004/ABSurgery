using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using SurgeonPortal.Models.ProfessionalStanding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.ProfessionalStanding
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/professional-standing/medical-license")]
	public class MedicalLicenseController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public MedicalLicenseController(
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
            [FromServices] IMedicalLicenseFactory medicalLicenseFactory,
            int licenseId)
        {
            var item = await medicalLicenseFactory.GetByIdAsync(licenseId);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MedicalLicenseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<MedicalLicenseModel>> GetMedicalLicense_GetByIdAsync(
            [FromServices] IMedicalLicenseFactory medicalLicenseFactory,
            int licenseId)
        {
            var item = await medicalLicenseFactory.GetByIdAsync(licenseId);
        
            return Ok(_mapper.Map<MedicalLicenseModel>(item));
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
            [FromServices] IMedicalLicenseFactory medicalLicenseFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] MedicalLicenseModel model)
        {
            var item = medicalLicenseFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<MedicalLicenseModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/professional-standing/medical-license/{item.LicenseId}"));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(MedicalLicenseModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IMedicalLicenseFactory medicalLicenseFactory,
            [FromBody] MedicalLicenseModel model,
            int licenseId)
        {
            var item = await medicalLicenseFactory.GetByIdAsync(licenseId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<MedicalLicenseModel>(_mapper, item);
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MedicalLicenseReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<MedicalLicenseReadOnlyModel>>> GetMedicalLicenseReadOnly_GetByUserIdAsync(
            [FromServices] IMedicalLicenseReadOnlyListFactory medicalLicenseReadOnlyListFactory)
        {
            var items = await medicalLicenseReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<MedicalLicenseReadOnlyModel>>(items));
        } 

        private void AssignCreateProperties(IMedicalLicense entity, MedicalLicenseModel model)
        {
            entity.IssuingStateId = model.IssuingStateId;
            entity.LicenseNumber = model.LicenseNumber;
            entity.LicenseTypeId = model.LicenseTypeId;
            entity.ReportingOrganization = model.ReportingOrganization;
            entity.IssueDate = model.IssueDate;
            entity.ExpireDate = model.ExpireDate;
        }

        private void AssignEditProperties(IMedicalLicense entity, MedicalLicenseModel model)
        {
            entity.IssuingStateId = model.IssuingStateId;
            entity.LicenseNumber = model.LicenseNumber;
            entity.LicenseTypeId = model.LicenseTypeId;
            entity.ReportingOrganization = model.ReportingOrganization;
            entity.IssueDate = model.IssueDate;
            entity.ExpireDate = model.ExpireDate;
        }
    }
}

