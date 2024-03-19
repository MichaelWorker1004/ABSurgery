using AutoMapper;
using Csla.Rules;
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
            
            IActionResult badRequestResult = await UpdateAsync<UserCredentialModel>(_mapper, item);

            BrokenRulesCollection coll = item.GetBrokenRules();

            if (coll.Count > 0)
                return BadRequest(coll[0].Description);
            else
                return badRequestResult;
        } 

        private void AssignEditProperties(IUserCredential entity, UserCredentialModel model)
        {
            entity.EmailAddress = model.EmailAddress;
            entity.Password = model.Password;
        }
    }
}

