using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Reports
{
	public interface IReportDal
	{
		Task<ReportDto> GetReportAsync(string query);
	}
}
