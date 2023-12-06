using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.EmailProvider
{
	public interface IEmailProvider
	{
		Task SendEmailAsync(TextEmailResource email);

		Task SendTemplateEmailAsync(TemplateEmailResource email);
	}
}
