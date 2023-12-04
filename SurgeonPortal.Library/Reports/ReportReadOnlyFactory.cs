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

	internal class ReportCriteriaBase
	{
		public string Type { get; set; }
	}
}
