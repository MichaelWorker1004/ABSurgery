using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Email
{
	public interface ISendEmailCommandFactory
	{
		Task<ISendEmailCommand> SendEmailCommandAsync(IEmail email);
	}
}
