using Microsoft.Extensions.Options;
using SurgeonPortal.DataAccess.Contracts.Reports;
using SurgeonPortal.Shared.Reports;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Reports
{
	public class ReportDal : IReportDal
	{
		private readonly ReportConfiguration _reportConfiguration;

		public ReportDal(IOptions<ReportConfiguration> reportConfiguration)
		{
			_reportConfiguration = reportConfiguration.Value;
		}

		public async Task<ReportDto> GetReportAsync(string query)
		{
			var httpClient = new HttpClient
			{
				BaseAddress = new Uri(_reportConfiguration.BaseUrl)
			};

			var response = await httpClient.GetAsync(query);
			
			if(!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new HttpRequestException($"Error retrieving report. {errorContent}", null, response.StatusCode);
			}

			var fileContent = await response.Content.ReadAsByteArrayAsync();
			var contentType = response.Content.Headers.ContentType?.MediaType;

			return new ReportDto
			{
				Data = fileContent,
				ContentType = contentType
			};
		}
	}
}
