using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class ResetForgotPasswordCommandFactory : IResetForgotPasswordCommandFactory
    {
        public async Task<IResetForgotPasswordCommand> ResetForgotPasswordAsync(
            Guid resetGUID,
            string newPassword)
        {
            if(!ResetForgotPasswordCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IResetForgotPasswordCommand))
        	    as ResetForgotPasswordCommand;
        
                cmd.ResetGUID = resetGUID;
                cmd.NewPassword = newPassword;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
