using Csla;
using SurgeonPortal.Library.Contracts.Email;

namespace SurgeonPortal.Library.Email
{
	public class AttachmentFactory : IAttachmentFactory
	{
		public IAttachment Create()
		{
			return DataPortal.Create<Attachment>();
		}
	}
}
