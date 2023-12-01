using Csla;
using SurgeonPortal.DataAccess.Contracts.Email;
using SurgeonPortal.Library.Contracts.Email;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Email
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	public class SendEmailCommand : YtgCommandBase<SendEmailCommand, int>, ISendEmailCommand
	{
		private readonly ISendEmailCommandDal _sendEmailCommandDal;

		public SendEmailCommand(IIdentityProvider identity,
			ISendEmailCommandDal sendEmailCommandDal) 
			: base(identity)
		{
			_sendEmailCommandDal = sendEmailCommandDal;
		}

		public static bool CanExecuteCommand()
		{
			return true;
		}

		public IEmail Email
		{
			get { return ReadProperty(EmailProperty); }
			internal set { LoadProperty(EmailProperty, value); }
		}
		public static readonly PropertyInfo<IEmail> EmailProperty = RegisterProperty<IEmail>(c => c.Email);

		[Execute]
		protected async Task Execute()
		{
			await _sendEmailCommandDal.SendEmailAsync(Email.To, Email.From, Email.Subject, Email.TemplateId);
		}
	}
}
