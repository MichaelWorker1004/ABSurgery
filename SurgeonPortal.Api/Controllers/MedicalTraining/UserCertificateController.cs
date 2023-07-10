using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Models.MedicalTraining;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.MedicalTraining
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/user-certificates")]
	public class UserCertificateController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public UserCertificateController(
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
            [FromServices] IUserCertificateFactory userCertificateFactory,
            int certificateId)
        {
            var item = await userCertificateFactory.GetByIdAsync(certificateId);
            item.Delete();
            await item.SaveAsync();
            return NoContent();
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserCertificateModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<UserCertificateModel>> GetUserCertificate_GetByIdAsync(
            [FromServices] IUserCertificateFactory userCertificateFactory,
            int certificateId)
        {
            var item = await userCertificateFactory.GetByIdAsync(certificateId);
        
            return Ok(_mapper.Map<UserCertificateModel>(item));
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
            [FromServices] IUserCertificateFactory userCertificateFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromForm] UserCertificateModel model)
        {
            var item = userCertificateFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<UserCertificateModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/user-certificates/{item.CertificateId}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserCertificateReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid")]
        public async Task<ActionResult<IEnumerable<UserCertificateReadOnlyModel>>> GetUserCertificateReadOnly_GetByUserIdAsync(
            [FromServices] IUserCertificateReadOnlyListFactory userCertificateReadOnlyListFactory)
        {
            var items = await userCertificateReadOnlyListFactory.GetByUserIdAsync();
        
            return Ok(_mapper.Map<IEnumerable<UserCertificateReadOnlyModel>>(items));
        } 

        private async void LoadDocument(IUserCertificate entity, UserCertificateModel model)
        {
            using (var stream = new MemoryStream())
            {
                await model.File.CopyToAsync(stream);
                var fileBytes = stream.ToArray();

                entity.LoadDocument(new MemoryStream(fileBytes));
            }
        }

        private void AssignCreateProperties(IUserCertificate entity, UserCertificateModel model)
        {
            entity.DocumentId = model.DocumentId;
            entity.CertificateTypeId = model.CertificateTypeId;
            entity.IssueDate = model.IssueDate;
            entity.CertificateNumber = model.CertificateNumber;
            LoadDocument(entity, model);
        }
    }
}

