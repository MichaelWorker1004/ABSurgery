using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class FellowshipProgramReadOnlyListFactory : IFellowshipProgramReadOnlyListFactory
    {
        public async Task<IFellowshipProgramReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<FellowshipProgramReadOnlyList>();
            
        }

            


    }
}
