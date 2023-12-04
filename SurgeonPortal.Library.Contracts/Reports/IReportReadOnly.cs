namespace SurgeonPortal.Library.Contracts.Reports
{
	public interface IReportReadOnly
	{
		byte[] Data { get; set; }
		string ContentType { get; }
	}
}
