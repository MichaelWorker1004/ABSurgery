using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Reports
{
	public interface IReportReadOnlyFactory
	{
		Task<IReportReadOnly> GetByInvoiceNumber(string invoiceNumber);

		Task<IReportReadOnly> GetAdmissionCardByExamId(int examId);
	}
}
