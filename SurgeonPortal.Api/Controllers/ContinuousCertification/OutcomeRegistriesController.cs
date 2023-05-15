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
	[Route("api/continuous-certification/outcome-registries")]
	public class OutcomeRegistriesController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public OutcomeRegistriesController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OutcomeRegistryModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<OutcomeRegistryModel>> GetOutcomeRegistry_GetByUserIdAsync(
            [FromServices] IOutcomeRegistryFactory outcomeRegistryFactory,
            int userId)
        {
            var item = await outcomeRegistryFactory.GetByUserIdAsync(userId);
        
            return Ok(_mapper.Map<OutcomeRegistryModel>(item));
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
            [FromServices] IOutcomeRegistryFactory outcomeRegistryFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] OutcomeRegistryModel model)
        {
            var item = outcomeRegistryFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<OutcomeRegistryModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/continuous-certification/outcome-registries/"));
        } 

        ///<summary>
        /// YtgIm 
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(OutcomeRegistryModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IOutcomeRegistryFactory outcomeRegistryFactory,
            [FromBody] OutcomeRegistryModel model,
            int userId)
        {
            var item = await outcomeRegistryFactory.GetByUserIdAsync(userId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<OutcomeRegistryModel>(_mapper, item);
        } 

        private void AssignCreateProperties(IOutcomeRegistry entity, OutcomeRegistryModel model)
        {
            entity.UserId = model.UserId;
            entity.SurgeonSpecificRegistry = model.SurgeonSpecificRegistry;
            entity.RegistryComments = model.RegistryComments;
            entity.RegisteredWithACHQC = model.RegisteredWithACHQC;
            entity.RegisteredWithCESQIP = model.RegisteredWithCESQIP;
            entity.RegisteredWithMBSAQIP = model.RegisteredWithMBSAQIP;
            entity.RegisteredWithABA = model.RegisteredWithABA;
            entity.RegisteredWithASBS = model.RegisteredWithASBS;
            entity.RegisteredWithStatewideCollaboratives = model.RegisteredWithStatewideCollaboratives;
            entity.RegisteredWithABMS = model.RegisteredWithABMS;
            entity.RegisteredWithNCDB = model.RegisteredWithNCDB;
            entity.RegisteredWithRQRS = model.RegisteredWithRQRS;
            entity.RegisteredWithNSQIP = model.RegisteredWithNSQIP;
            entity.RegisteredWithNTDB = model.RegisteredWithNTDB;
            entity.RegisteredWithSTS = model.RegisteredWithSTS;
            entity.RegisteredWithTQIP = model.RegisteredWithTQIP;
            entity.RegisteredWithUNOS = model.RegisteredWithUNOS;
            entity.RegisteredWithNCDR = model.RegisteredWithNCDR;
            entity.RegisteredWithSVS = model.RegisteredWithSVS;
            entity.RegisteredWithELSO = model.RegisteredWithELSO;
            entity.UserConfirmed = model.UserConfirmed;
            entity.UserConfirmedDateUtc = model.UserConfirmedDateUtc;
        }

        private void AssignEditProperties(IOutcomeRegistry entity, OutcomeRegistryModel model)
        {
            entity.SurgeonSpecificRegistry = model.SurgeonSpecificRegistry;
            entity.RegistryComments = model.RegistryComments;
            entity.RegisteredWithACHQC = model.RegisteredWithACHQC;
            entity.RegisteredWithCESQIP = model.RegisteredWithCESQIP;
            entity.RegisteredWithMBSAQIP = model.RegisteredWithMBSAQIP;
            entity.RegisteredWithABA = model.RegisteredWithABA;
            entity.RegisteredWithASBS = model.RegisteredWithASBS;
            entity.RegisteredWithStatewideCollaboratives = model.RegisteredWithStatewideCollaboratives;
            entity.RegisteredWithABMS = model.RegisteredWithABMS;
            entity.RegisteredWithNCDB = model.RegisteredWithNCDB;
            entity.RegisteredWithRQRS = model.RegisteredWithRQRS;
            entity.RegisteredWithNSQIP = model.RegisteredWithNSQIP;
            entity.RegisteredWithNTDB = model.RegisteredWithNTDB;
            entity.RegisteredWithSTS = model.RegisteredWithSTS;
            entity.RegisteredWithTQIP = model.RegisteredWithTQIP;
            entity.RegisteredWithUNOS = model.RegisteredWithUNOS;
            entity.RegisteredWithNCDR = model.RegisteredWithNCDR;
            entity.RegisteredWithSVS = model.RegisteredWithSVS;
            entity.RegisteredWithELSO = model.RegisteredWithELSO;
            entity.UserConfirmed = model.UserConfirmed;
            entity.UserConfirmedDateUtc = model.UserConfirmedDateUtc;
            entity.UserId = model.UserId;
        }
    }
}

