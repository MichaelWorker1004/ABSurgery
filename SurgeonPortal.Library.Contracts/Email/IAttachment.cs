namespace SurgeonPortal.Library.Contracts.Email
{
	public interface IAttachment
	{
		public string Filename { get; set; }
		public byte[] Content { get; set; }
		public string ContentType { get; set; }
	}
}
