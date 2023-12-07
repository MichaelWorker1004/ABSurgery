using System.IO;

namespace SurgeonPortal.Library.Contracts.EmailProvider
{
	public class AttachmentResource
	{
		public string Filename { get; set; }
		public byte[] Content { get; set; }
		public string ContentType { get; set; }
	}
}
