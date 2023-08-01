using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class ExamSessionSkipCommandFactory : IExamSessionSkipCommandFactory
    {
        public async Task<IExamSessionSkipCommand> SkipExamSessionAsync(int examScheduleId)
        {
            if(!ExamSessionSkipCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IExamSessionSkipCommand))
        	    as ExamSessionSkipCommand;
        
                cmd.ExamScheduleId = examScheduleId;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
