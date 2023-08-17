using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class FellowshipTypeReadOnlyListFactory : IFellowshipTypeReadOnlyListFactory
    {
        public async Task<IFellowshipTypeReadOnlyList> GetAsync()
        {
            
            return await DataPortal.FetchAsync<FellowshipTypeReadOnlyList>();
            
        }

            


    }
}
