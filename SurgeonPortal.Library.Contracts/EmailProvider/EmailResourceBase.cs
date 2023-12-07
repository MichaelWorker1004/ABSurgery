using System.Collections.Generic;

namespace SurgeonPortal.Library.Contracts.EmailProvider
{
	public class EmailResourceBase
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Subject { get; set; }
		public IList<AttachmentResource> Attachments { get; set; }
	}
}
