using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class VerifyForgotGuidCommandFactory : IVerifyForgotGuidCommandFactory
    {
        public async Task<IVerifyForgotGuidCommand> VerifyForgotPasswordGuidAsync(Guid resetGUID)
        {
            if(!VerifyForgotGuidCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IVerifyForgotGuidCommand))
        	    as VerifyForgotGuidCommand;
        
                cmd.ResetGUID = resetGUID;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
