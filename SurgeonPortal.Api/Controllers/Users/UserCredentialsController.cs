using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Models.Users;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.Users
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/users/credentials")]
	public class UserCredentialsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public UserCredentialsController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserCredentialModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<UserCredentialModel>> GetUserCredential_GetByUserIdAsync(
            [FromServices] IUserCredentialFactory userCredentialFactory)
        {
            var item = await userCredentialFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<UserCredentialModel>(item));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(UserCredentialModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IUserCredentialFactory userCredentialFactory,
            [FromBody] UserCredentialModel model)
        {
            var item = await userCredentialFactory.GetByUserIdAsync();
            AssignEditProperties(item, model);
            
            return await UpdateAsync<UserCredentialModel>(_mapper, item);
        } 

        private void AssignEditProperties(IUserCredential entity, UserCredentialModel model)
        {
            entity.EmailAddress = model.EmailAddress;
            entity.Password = model.Password;
        }
    }
}

