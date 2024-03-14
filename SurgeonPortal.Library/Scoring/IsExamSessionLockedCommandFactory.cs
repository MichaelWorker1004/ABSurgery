using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class IsExamSessionLockedCommandFactory : IIsExamSessionLockedCommandFactory
    {
        public IIsExamSessionLockedCommand IsExamSessionLocked(int examCaseId)
        {
            if(!IsExamSessionLockedCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }

            if(examCaseId == 0)
            {
                return new object() as IsExamSessionLockedCommand;
            }
            else
            {
                var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IIsExamSessionLockedCommand))
                    as IsExamSessionLockedCommand;

                cmd.ExamCaseId = examCaseId;

                return DataPortal.Execute(cmd);
            }
            

            //return examCaseId == 0 ? cmd : DataPortal.Execute(cmd);
        }
    }
}
