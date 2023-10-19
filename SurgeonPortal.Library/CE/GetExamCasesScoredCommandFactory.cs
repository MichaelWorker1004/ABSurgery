using Csla;
using SurgeonPortal.Library.Contracts.CE;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.CE
{
    public class GetExamCasesScoredCommandFactory : IGetExamCasesScoredCommandFactory
    {
        public IGetExamCasesScoredCommand GetExamCasesScored(int examScheduleId)
        {
            if(!GetExamCasesScoredCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IGetExamCasesScoredCommand))
        	    as GetExamCasesScoredCommand;
        
                cmd.ExamScheduleId = examScheduleId;
        
            return DataPortal.Execute(cmd);
        }


    }
}
