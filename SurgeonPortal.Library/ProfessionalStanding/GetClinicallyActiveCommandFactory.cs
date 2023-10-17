using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class GetClinicallyActiveCommandFactory : IGetClinicallyActiveCommandFactory
    {
        public IGetClinicallyActiveCommand GetClinicallyActiveByUserId(int userId)
        {
            if(!GetClinicallyActiveCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IGetClinicallyActiveCommand))
        	    as GetClinicallyActiveCommand;
        
                cmd.UserId = userId;
        
            return DataPortal.Execute(cmd);
        }


    }
}
