using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.DataAccess.Contracts.Storage;
using SurgeonPortal.DataAccess.Documents;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.Documents;
using SurgeonPortal.Library.MedicalTraining;
using SurgeonPortal.Models.Documents;
using SurgeonPortal.Models.MedicalTraining;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;
using static Dapper.SqlMapper;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SurgeonPortal.Api.Controllers.MedicalTraining
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/user-certificates")]
	public class UserCertificateController : YtgControllerBase
	{
        private readonly IMapper _mapper;
        private readonly IDocumentDal _documentDal;
        private readonly IUserCertificateDal _userCertificateDal;
        private readonly IStorageDal _storageDal;

        public UserCertificateController(
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            IDocumentDal documentDal,
            IUserCertificateDal userCertificateDal,
            IStorageDal storageDal)
            : base(webHostEnvironment)
        {
            _mapper = mapper;
            _documentDal = documentDal;
            _userCertificateDal = userCertificateDal;
            _storageDal = storageDal;
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
        //[MapToApiVersion("1")]
        //[ProducesResponseType(201)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[HttpPost("")]
        //public async Task<IActionResult> CreateAsync(
        //    [FromServices] IUserCertificateFactory userCertificateFactory,
        //    [FromServices] IAbsoluteUriProvider absoluteUriProvider,
        //    [FromForm] UserCertificateModel model)
        //{
        //    var item = userCertificateFactory.Create();
        //    AssignCreateProperties(item, model);

        //    return await CreateAsync<UserCertificateModel>(
        //        _mapper,
        //        item,
        //        absoluteUriProvider.GetAbsoluteUri($"/api/user-certificates/{item.CertificateId}"));
        //}

        [MapToApiVersion("1")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("")]
        public async Task<ActionResult<DocumentModel>> CreateAsync(
            [FromForm] UserCertificateFileModel model)
        {
            var userId = User?.Claims.Where(c => c.Type.Contains("UserId")).FirstOrDefault();

            //add document 
            DocumentDto documentDto = new DocumentDto();

            documentDto.DocumentTypeId = (int)DocumentTypes.Certificate;
            documentDto.DocumentName = $"Certificate-{Enum.GetName(typeof(CertificateTypes), model.CertificateTypeId)}-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}";
            documentDto.InternalViewOnly = false;
            documentDto.UploadedBy = User?.Identity?.Name;
            documentDto.UserId = Convert.ToInt32(userId?.Value);
            documentDto.CreatedByUserId = Convert.ToInt32(userId?.Value);
            documentDto.LastUpdatedByUserId = Convert.ToInt32(userId?.Value);
            documentDto.StreamId = Guid.NewGuid();
            documentDto.InternalViewOnly = false;
            documentDto.UploadedDateUtc = DateTime.UtcNow;

            documentDto = await _documentDal.InsertAsync(documentDto);

            //add certificate
            UserCertificateDto userCertificateDto = new UserCertificateDto();

            userCertificateDto.UserId = Convert.ToInt32(userId?.Value);
            userCertificateDto.CreatedByUserId = Convert.ToInt32(userId?.Value);
            userCertificateDto.LastUpdatedByUserId = Convert.ToInt32(userId?.Value);
            userCertificateDto.DocumentId = documentDto.Id;
            userCertificateDto.CertificateTypeId = model.CertificateTypeId;
            userCertificateDto.IssueDate = DateTime.UtcNow;
            userCertificateDto.CertificateNumber = "";

            await _userCertificateDal.InsertAsync(userCertificateDto);

            //add file to azure
            using (var stream = new MemoryStream())
            {
                await model.File.CopyToAsync(stream);

                if(stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);

                await _storageDal.SaveAsync(stream, $"{documentDto.StreamId.ToString()}.pdf");
            }

            return Ok();
        }

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserCertificateReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-userid-and-typeid")]
        public async Task<ActionResult<IEnumerable<UserCertificateReadOnlyModel>>> GetUserCertificateReadOnly_GetByUserAndTypeAsync(
            [FromServices] IUserCertificateReadOnlyListFactory userCertificateReadOnlyListFactory,
            int certificateTypeId)
        {
            var items = await userCertificateReadOnlyListFactory.GetByUserAndTypeAsync(certificateTypeId);
        
            return Ok(_mapper.Map<IEnumerable<UserCertificateReadOnlyModel>>(items));
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
            if (model.File == null)
                return;

            try
            {
                using (var stream = new MemoryStream())
                {
                    await model.File.CopyToAsync(stream);
                    var fileBytes = stream.ToArray();

                    entity.LoadDocument(new MemoryStream(fileBytes));
                }
            }
            catch 
            {
                return;
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

