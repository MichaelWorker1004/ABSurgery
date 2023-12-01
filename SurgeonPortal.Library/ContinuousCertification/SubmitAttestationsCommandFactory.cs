using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class SubmitAttestationsCommandFactory : ISubmitAttestationsCommandFactory
    {
        public async Task<ISubmitAttestationsCommand> GetExamCasesScoredAsync(
            DateTime sigReceive,
            DateTime certnoticeReceive)
        {
            if(!SubmitAttestationsCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(ISubmitAttestationsCommand))
        	    as SubmitAttestationsCommand;
        
                cmd.SigReceive = sigReceive;
                cmd.CertnoticeReceive = certnoticeReceive;
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
