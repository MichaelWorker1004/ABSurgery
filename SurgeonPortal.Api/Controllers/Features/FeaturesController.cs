using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SurgeonPortal.Api.Configuration;
using SurgeonPortal.Models.Features;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Features
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/features")]
    public class FeaturesController : YtgControllerBase
    {
        private readonly FeatureFlagConfiguration _featureFlags;
        private readonly IMapper _mapper;

        public FeaturesController(IWebHostEnvironment webHostEnvironment,
            IOptions<FeatureFlagConfiguration> featureFlagsConfiguration,
            IMapper mapper) 
            : base(webHostEnvironment)
        {
            _featureFlags = featureFlagsConfiguration.Value;
            _mapper = mapper;
        }

        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagsModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public IActionResult Flags()
        {
            return Ok(_mapper.Map<FeatureFlagsModel>(_featureFlags));
        }
    }
}
