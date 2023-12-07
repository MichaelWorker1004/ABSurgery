using Csla;
using SurgeonPortal.Library.Contracts.Email;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Email
{
	public  class SendEmailcommandFactory : ISendEmailCommandFactory
	{
		public async Task<ISendEmailCommand> SendEmailCommandAsync(IEmail email)
		{
			if(!SendEmailCommand.CanExecuteCommand())
			{
				throw new System.Security.SecurityException("Not authorized to execute command");
			}

			var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(ISendEmailCommand))
				as SendEmailCommand;

			cmd.Email = email;

			return await DataPortal.ExecuteAsync(cmd);
		}
	}
}
