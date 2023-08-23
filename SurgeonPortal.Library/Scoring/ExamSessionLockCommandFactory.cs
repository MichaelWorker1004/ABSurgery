using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class ExamSessionLockCommandFactory : IExamSessionLockCommandFactory
    {
        public async Task<IExamSessionLockCommand> LockExamSessionAsync(int examscheduleId)
        {
            if(!ExamSessionLockCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IExamSessionLockCommand))
        	    as ExamSessionLockCommand;
        
                cmd.ExamscheduleId = examscheduleId;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
