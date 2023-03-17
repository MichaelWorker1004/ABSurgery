using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Users
{
    public class PasswordValidationCommandFactory : IPasswordValidationCommandFactory
    {
        public async Task<IPasswordValidationCommand> ValidateAsync(
            int userId,
            string password)
        {
            if(!PasswordValidationCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IPasswordValidationCommand))
        	    as PasswordValidationCommand;
        
                cmd.UserId = userId;
                cmd.Password = password;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
