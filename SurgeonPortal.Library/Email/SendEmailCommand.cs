using Csla;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Contracts.EmailProvider;
using System;
using System.Collections.Generic;
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
		private readonly IEmailProvider _emailProvider;

		public SendEmailCommand(IIdentityProvider identity,
			IEmailProvider emailProvider) 
			: base(identity)
		{
			_emailProvider = emailProvider;
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
			if(!string.IsNullOrEmpty(Email.TemplateId))
			{
				var emailResource = new TemplateEmailResource()
				{
					From = Email.From,
					To = Email.To,
					Subject = Email.Subject,
					Template = Email.TemplateId,
					TemplateData = Email.TemplateData
				};

				AddAttachment(emailResource);

				await _emailProvider.SendTemplateEmailAsync(emailResource);
			}
			else
			{
				var emailResource = new TextEmailResource()
				{
					From = Email.From,
					To = Email.To,
					Subject = Email.Subject,
					Text = Email.PlainTextContent
				};

				AddAttachment(emailResource);

				await _emailProvider.SendEmailAsync(emailResource);
			}
		}

		private void AddAttachment(EmailResourceBase emailResource)
		{
			if (Email.Attachment != null)
			{
				emailResource.Attachments = new List<AttachmentResource>
				{
					new AttachmentResource
					{
						Filename = Email.Attachment.Filename,
						Content = Email.Attachment.Content,
						ContentType = Email.Attachment.ContentType
					}
				};
			}
		}
	}
}
