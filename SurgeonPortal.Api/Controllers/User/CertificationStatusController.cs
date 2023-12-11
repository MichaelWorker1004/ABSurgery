using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.User;
using SurgeonPortal.Models.User;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.User
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/user/certification-status")]
	public class CertificationStatusController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public CertificationStatusController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CertificationStatusReadOnlyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<CertificationStatusReadOnlyModel>> GetCertificationStatusReadOnly_GetByUserIdAsync(
            [FromServices] ICertificationStatusReadOnlyFactory certificationStatusReadOnlyFactory)
        {
            var item = await certificationStatusReadOnlyFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<CertificationStatusReadOnlyModel>(item));
        } 
    }
}

