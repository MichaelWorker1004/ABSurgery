using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ResidencyProgramReadOnlyListFactory : IResidencyProgramReadOnlyListFactory
    {
        public async Task<IResidencyProgramReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ResidencyProgramReadOnlyList>();
            
        }

            


    }
}
