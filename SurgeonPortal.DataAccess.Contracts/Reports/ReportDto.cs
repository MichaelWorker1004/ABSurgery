using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Reports
{
	public class ReportDto : YtgBusinessBaseDto
	{
		public byte[] Data { get; set; }
		public string ContentType { get; set; }
	}
}
