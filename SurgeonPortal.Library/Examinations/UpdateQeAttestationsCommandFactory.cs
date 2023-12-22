using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class UpdateQeAttestationsCommandFactory : IUpdateQeAttestationsCommandFactory
    {
        public async Task<IUpdateQeAttestationsCommand> UpdateQeAttestationsAsync(int examId)
        {
            if(!UpdateQeAttestationsCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IUpdateQeAttestationsCommand))
        	    as UpdateQeAttestationsCommand;
        
                cmd.ExamId = examId;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
