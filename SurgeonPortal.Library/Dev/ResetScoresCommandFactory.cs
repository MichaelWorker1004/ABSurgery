using Csla;
using SurgeonPortal.Library.Contracts.Dev;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Dev
{
    public class ResetScoresCommandFactory : IResetScoresCommandFactory
    {
        public async Task<IResetScoresCommand> ResetExamScoresAsync()
        {
            if(!ResetScoresCommand.CanExecuteCommand())
            {
                throw new System.Security.SecurityException("Not authorized to execute command");
            }
        
            var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IResetScoresCommand))
        	    as ResetScoresCommand;
        
        
            return await DataPortal.ExecuteAsync(cmd);
        }


    }
}
