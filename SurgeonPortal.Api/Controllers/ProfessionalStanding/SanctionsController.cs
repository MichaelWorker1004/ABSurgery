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
	[Route("api/professional-standing/sanctions")]
	public class SanctionsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public SanctionsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SanctionsModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<SanctionsModel>> GetSanctions_GetByUserIdAsync(
            [FromServices] ISanctionsFactory sanctionsFactory)
        {
            var item = await sanctionsFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<SanctionsModel>(item));
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
            [FromServices] ISanctionsFactory sanctionsFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] SanctionsModel model)
        {
            var item = sanctionsFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<SanctionsModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/professional-standing/sanctions/{item.Id}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(SanctionsModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] ISanctionsFactory sanctionsFactory,
            [FromBody] SanctionsModel model)
        {
            var item = await sanctionsFactory.GetByUserIdAsync();
            AssignEditProperties(item, model);
            
            return await UpdateAsync<SanctionsModel>(_mapper, item);
        } 

        private void AssignCreateProperties(ISanctions entity, SanctionsModel model)
        {
            entity.HadDrugAlchoholTreatment = model.HadDrugAlchoholTreatment;
            entity.HadHospitalPrivilegesDenied = model.HadHospitalPrivilegesDenied;
            entity.HadLicenseRestricted = model.HadLicenseRestricted;
            entity.HadHospitalPrivilegesRestricted = model.HadHospitalPrivilegesRestricted;
            entity.HadFelonyConviction = model.HadFelonyConviction;
            entity.HadCensure = model.HadCensure;
            entity.Explanation = model.Explanation;
        }

        private void AssignEditProperties(ISanctions entity, SanctionsModel model)
        {
            entity.HadDrugAlchoholTreatment = model.HadDrugAlchoholTreatment;
            entity.HadHospitalPrivilegesDenied = model.HadHospitalPrivilegesDenied;
            entity.HadLicenseRestricted = model.HadLicenseRestricted;
            entity.HadHospitalPrivilegesRestricted = model.HadHospitalPrivilegesRestricted;
            entity.HadFelonyConviction = model.HadFelonyConviction;
            entity.HadCensure = model.HadCensure;
            entity.Explanation = model.Explanation;
        }
    }
}

