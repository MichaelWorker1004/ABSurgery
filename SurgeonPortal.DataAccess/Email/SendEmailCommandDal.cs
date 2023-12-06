using SendGrid;
using SendGrid.Helpers.Mail;
using SurgeonPortal.DataAccess.Contracts.Email;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Email
{
	public class SendEmailCommandDal : ISendEmailCommandDal
	{
		private readonly ISendGridClient _sendGridClient;
		public SendEmailCommandDal(ISendGridClient sendGridClient)
		{
			_sendGridClient = sendGridClient;
		}

		public async Task SendEmailAsync(string toEmail, string fromEmail, string subject, string templateId, string plainTextContent)
		{
			SendGridMessage msg;

			if (!string.IsNullOrEmpty(templateId))
			{
				msg = MailHelper.CreateSingleTemplateEmail(new EmailAddress(fromEmail), new EmailAddress(toEmail), subject, templateId);
			}
			else
			{
				msg = MailHelper.CreateSingleEmail(new EmailAddress(fromEmail), new EmailAddress(toEmail), subject, plainTextContent, null);
			}

			await _sendGridClient.SendEmailAsync(msg);
		}
	}
}
