using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class UserLoginAuditCommandFactory : IUserLoginAuditCommandFactory
    {
        public async Task<IUserLoginAuditCommand> AuditAsync(
            int userId,
            string emailAddress,
            int applicationId,
            string loginIpAddress,
            string loginUserAgent,
            bool loginSuccess,
            string loginFailureReason)
        {
            if(!UserLoginAuditCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IUserLoginAuditCommand))
        	    as UserLoginAuditCommand;
        
                cmd.UserId = userId;
                cmd.EmailAddress = emailAddress;
                cmd.ApplicationId = applicationId;
                cmd.LoginIpAddress = loginIpAddress;
                cmd.LoginUserAgent = loginUserAgent;
                cmd.LoginSuccess = loginSuccess;
                cmd.LoginFailureReason = loginFailureReason;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
