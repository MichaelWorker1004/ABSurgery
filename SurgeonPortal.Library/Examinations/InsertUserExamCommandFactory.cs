using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class InsertUserExamCommandFactory : IInsertUserExamCommandFactory
    {
        public async Task<IInsertUserExamCommand> InsertUserExamAsync(int examHeaderId)
        {
            if(!InsertUserExamCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IInsertUserExamCommand))
        	    as InsertUserExamCommand;
        
                cmd.ExamHeaderId = examHeaderId;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
