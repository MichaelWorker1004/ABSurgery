using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class CreateForgotPasswordCommandFactory : ICreateForgotPasswordCommandFactory
    {
        public async Task<ICreateForgotPasswordCommand> SendForgotPasswordEmailAsync(string userName)
        {
            if(!CreateForgotPasswordCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(ICreateForgotPasswordCommand))
        	    as CreateForgotPasswordCommand;
        
                cmd.UserName = userName;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
