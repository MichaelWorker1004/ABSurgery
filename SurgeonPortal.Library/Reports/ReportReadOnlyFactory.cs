using Csla;
using SurgeonPortal.Library.Contracts.Reports;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Reports
{
	public class ReportReadOnlyFactory : IReportReadOnlyFactory
	{
		public async Task<IReportReadOnly> GetByInvoiceNumber(string invoiceNumber)
		{
			if(string.IsNullOrWhiteSpace(invoiceNumber))
			{
				throw new FactoryInvalidCriteriaException("invoiceNumber is a required field");
			}

			return await DataPortal.FetchAsync<ReportReadOnly>(
				new GetByInvoiceNumberCriteria(invoiceNumber));
		}

		public async Task<IReportReadOnly> GetAdmissionCardByExamId(int examId)
		{
			if(examId <= 0)
			{
				throw new FactoryInvalidCriteriaException("examId is a required field");
			}

			return await DataPortal.FetchAsync<ReportReadOnly>(
				new GetAdmissionCardByExamIdCriteria(examId));
		}
	}

	[Serializable]
	internal class GetByInvoiceNumberCriteria : ReportCriteriaBase
	{
		public string InvoiceNumber { get; set; }

		public GetByInvoiceNumberCriteria(string invoiceNumber)
		{
			Type = "invoice";
			InvoiceNumber = invoiceNumber;
		}
	}

	[Serializable]
	internal class GetAdmissionCardByExamIdCriteria : ReportCriteriaBase
	{
		public int ExamId { get; set; }

		public GetAdmissionCardByExamIdCriteria(int examId)
		{
			Type = "adm_auth";
			ExamId = examId;
		}
	}

	internal class ReportCriteriaBase
	{
		public string Type { get; set; }
	}
}
