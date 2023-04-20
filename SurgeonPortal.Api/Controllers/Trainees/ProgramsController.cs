using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Trainees;
using SurgeonPortal.Models.Trainees;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Trainees
{
  [ApiVersion("1")]
  [ApiController]
  [Produces("application/json")]
  [Route("api/trainees/programs")]
  public class ProgramsController : YtgControllerBase
  {
    private readonly IMapper _mapper;

    public ProgramsController(
        IWebHostEnvironment webHostEnvironment,
        IMapper mapper)
        : base(webHostEnvironment)
    {
      _mapper = mapper;
    }

    ///<summary>
    /// YtgIm 
    ///<summary>
    [Authorize]
    [MapToApiVersion("1")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgramReadOnlyModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("")]
    public async Task<ActionResult<ProgramReadOnlyModel>> GetProgramReadOnly_GetByUserIdAsync(
        [FromServices] IProgramReadOnlyFactory programReadOnlyFactory,
        int userId)
    {
      var item = await programReadOnlyFactory.GetByUserIdAsync(userId);

      return Ok(_mapper.Map<ProgramReadOnlyModel>(item));
    }
  }
}

