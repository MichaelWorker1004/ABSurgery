using Csla;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    public class OverlapConflictCommandFactory : IOverlapConflictCommandFactory
    {
        public IOverlapConflictCommand CheckOverlapConflicts(
            int userId,
            DateTime startDate,
            DateTime endDate)
        {
            if(!OverlapConflictCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IOverlapConflictCommand))
        	    as OverlapConflictCommand;
        
                cmd.UserId = userId;
                cmd.StartDate = startDate;
                cmd.EndDate = endDate;
        
            return DataPortal.Execute(cmd);
        }


    }
}
