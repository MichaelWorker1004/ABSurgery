using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class PasswordResetCommandFactory : IPasswordResetCommandFactory
    {
        public async Task<IPasswordResetCommand> ResetPasswordAsync(
            string oldPassword,
            string newPassword)
        {
            if(!PasswordResetCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IPasswordResetCommand))
        	    as PasswordResetCommand;
        
                cmd.OldPassword = oldPassword;
                cmd.NewPassword = newPassword;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
