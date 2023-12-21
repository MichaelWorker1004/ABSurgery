using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class ForgotUsernameCommandFactory : IForgotUsernameCommandFactory
    {
        public async Task<IForgotUsernameCommand> SendForgotUsernameEmailAsync(string email)
        {
            if(!ForgotUsernameCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IForgotUsernameCommand))
        	    as ForgotUsernameCommand;
        
                cmd.Email = email;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
