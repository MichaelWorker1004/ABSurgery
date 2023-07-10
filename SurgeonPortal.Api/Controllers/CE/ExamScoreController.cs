using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.CE;
using SurgeonPortal.Models.CE;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;
using Ytg.AspNetCore.Helpers;

namespace SurgeonPortal.Api.Controllers.CE
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/exam-scores")]
	public class ExamScoreController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public ExamScoreController(
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamScoreModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("by-id")]
        public async Task<ActionResult<ExamScoreModel>> GetExamScore_GetByIdAsync(
            [FromServices] IExamScoreFactory examScoreFactory,
            int examScheduleScoreId)
        {
            var item = await examScoreFactory.GetByIdAsync(examScheduleScoreId);
        
            return Ok(_mapper.Map<ExamScoreModel>(item));
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
            [FromServices] IExamScoreFactory examScoreFactory,
            [FromServices] IAbsoluteUriProvider absoluteUriProvider,
            [FromBody] ExamScoreModel model)
        {
            var item = examScoreFactory.Create();
            AssignCreateProperties(item, model);
        
            return await CreateAsync<ExamScoreModel>(
                _mapper,
                item,
                absoluteUriProvider.GetAbsoluteUri($"/api/exam-scores/{item.ExamScheduleScoreId}"));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ExamScoreModel))]
        [ProducesResponseType(401)]
        [HttpPut("")]
        public async Task<IActionResult> EditAsync(
            [FromServices] IExamScoreFactory examScoreFactory,
            [FromBody] ExamScoreModel model,
            int examScheduleScoreId)
        {
            var item = await examScoreFactory.GetByIdAsync(examScheduleScoreId);
            AssignEditProperties(item, model);
            
            return await UpdateAsync<ExamScoreModel>(_mapper, item);
        } 

        private void AssignCreateProperties(IExamScore entity, ExamScoreModel model)
        {
            entity.ExamScheduleId = model.ExamScheduleId;
        }

        private void AssignEditProperties(IExamScore entity, ExamScoreModel model)
        {
            entity.ExaminerScore = model.ExaminerScore;
        }
    }
}

