using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Reports;
using SurgeonPortal.Models.Examinations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Examinations
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/examinations/admission-card")]
	public class AdmissionCardController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public AdmissionCardController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdmissionCardAvailabilityReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("availability")]
        public async Task<ActionResult<AdmissionCardAvailabilityReadOnlyModel>> GetAdmissionCardAvailabilityReadOnly_GetByExamIdAsync(
            [FromServices] IAdmissionCardAvailabilityReadOnlyFactory admissionCardAvailabilityReadOnlyFactory,
            int examID)
        {
            var item = await admissionCardAvailabilityReadOnlyFactory.GetByExamIdAsync(examID);
        
            return Ok(_mapper.Map<AdmissionCardAvailabilityReadOnlyModel>(item));
        }

        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("document")]
        public async Task<ActionResult<AdmissionCardAvailabilityReadOnlyModel>> GetAdmissionCardDocument(
            [FromServices] IReportReadOnlyFactory reportReadOnlyFactory,
            string examCode)
        {
            var report = await reportReadOnlyFactory.GetAdmissionCardByExamId(examCode);

            return File(report.Data, report.ContentType);
        }
    }
}

