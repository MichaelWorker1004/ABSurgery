using SendGrid;
using SendGrid.Helpers.Mail;
using SurgeonPortal.Library.Contracts.EmailProvider;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.EmailProvider.SendGrid
{
	public class SendGridEmailProvider : IEmailProvider
	{
		private readonly ISendGridClient _sendGridClient;

		public SendGridEmailProvider(ISendGridClient sendGridClient)
		{
			_sendGridClient = sendGridClient;
		}

		public async Task SendEmailAsync(TextEmailResource email)
		{
			var msg = MailHelper.CreateSingleEmail(new EmailAddress(email.From), new EmailAddress(email.To), email.Subject, email.Text, null);

			await SendEmail(email, msg);
		}

		public async Task SendTemplateEmailAsync(TemplateEmailResource email)
		{
			var msg = MailHelper.CreateSingleTemplateEmail(new EmailAddress(email.From), new EmailAddress(email.To), email.Template, email.TemplateData);

			await SendEmail(email, msg);
		}

		private async Task SendEmail(EmailResourceBase email, SendGridMessage msg)
		{
			if (email.Attachments?.Any() == true)
			{
				foreach (var attachment in email.Attachments)
				{
					msg.AddAttachment(attachment.Filename, Convert.ToBase64String(attachment.Content), attachment.ContentType);
				}
			}

			await _sendGridClient.SendEmailAsync(msg);
		}
	}
}
