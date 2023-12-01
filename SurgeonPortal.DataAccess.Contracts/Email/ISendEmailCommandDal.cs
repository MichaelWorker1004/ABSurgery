using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Email
{
	public interface ISendEmailCommandDal
	{
		Task SendEmailAsync(
			string toEmail,
			string fromEmail,
			string subject,
			string templateId);
	}
}
