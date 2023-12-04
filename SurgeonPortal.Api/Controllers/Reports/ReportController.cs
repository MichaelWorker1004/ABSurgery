using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Reports;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Reports
{
	[ApiVersion("1")]
	[ApiController]
	[Route("api/reports")]
	public class ReportController : YtgControllerBase
	{
		public ReportController(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
		{
		}

		[MapToApiVersion("1")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[HttpGet("invoice")]
		public async Task<ActionResult<FileContentResult>> GetReport(
			[FromServices] IReportReadOnlyFactory reportReadOnlyFactory,
			[FromQuery] string invoiceNumber)
		{
			var report = await reportReadOnlyFactory.GetByInvoiceNumber(invoiceNumber);

			return File(report.Data, report.ContentType);
		}
	}
}
