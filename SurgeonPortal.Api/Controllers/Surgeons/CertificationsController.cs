using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Surgeons;
using SurgeonPortal.Models.Surgeons;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Surgeons
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/surgeons/certifications")]
	public class CertificationsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CertificationsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CertificationReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CertificationReadOnlyModel>>> GetCertificationReadOnly_GetByUserIdAsync(
            [FromServices] ICertificationReadOnlyListFactory certificationReadOnlyListFactory)
        {
            var items = await certificationReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<CertificationReadOnlyModel>>(items));
        } 
    }
}

