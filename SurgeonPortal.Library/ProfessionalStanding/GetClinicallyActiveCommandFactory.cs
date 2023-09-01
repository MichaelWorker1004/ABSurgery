using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class GetClinicallyActiveCommandFactory : IGetClinicallyActiveCommandFactory
    {
        public IGetClinicallyActiveCommand GetClinicallyActiveByUserId()
        {
            if(!GetClinicallyActiveCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IGetClinicallyActiveCommand))
        	    as GetClinicallyActiveCommand;
        
        
            return DataPortal.Execute(cmd);
        }


    }
}
