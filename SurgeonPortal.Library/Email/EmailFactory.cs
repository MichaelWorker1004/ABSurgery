using Csla;
using SurgeonPortal.Library.Contracts.Email;

namespace SurgeonPortal.Library.Email
{
	public class EmailFactory : IEmailFactory
	{
		public IEmail Create()
		{
			return DataPortal.Create<Email>();
		}
	}
}
