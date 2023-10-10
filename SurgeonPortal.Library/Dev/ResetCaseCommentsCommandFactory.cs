using Csla;
using SurgeonPortal.Library.Contracts.Dev;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Dev
{
    public class ResetCaseCommentsCommandFactory : IResetCaseCommentsCommandFactory
    {
        public async Task<IResetCaseCommentsCommand> ResetCaseCommentsAsync(int examinerUserId)
        {
            if(!ResetCaseCommentsCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IResetCaseCommentsCommand))
        	    as ResetCaseCommentsCommand;
        
                cmd.ExaminerUserId = examinerUserId;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
