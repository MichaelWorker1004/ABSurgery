using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class RegistrationCompleteCommandFactory : IRegistrationCompleteCommandFactory
    {
        public async Task<IRegistrationCompleteCommand> CompleteRegistrationAsync(int examHeaderId)
        {
            if(!RegistrationCompleteCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IRegistrationCompleteCommand))
        	    as RegistrationCompleteCommand;
        
                cmd.ExamHeaderId = examHeaderId;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
