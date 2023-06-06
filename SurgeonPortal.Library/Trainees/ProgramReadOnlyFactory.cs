using Csla;
using SurgeonPortal.Library.Contracts.Trainees;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Trainees
{
    public class ProgramReadOnlyFactory : IProgramReadOnlyFactory
    {
        public async Task<IProgramReadOnly> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<ProgramReadOnly>();
            
        }


        
            


    }
}
