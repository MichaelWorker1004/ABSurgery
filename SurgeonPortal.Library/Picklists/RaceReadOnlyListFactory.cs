using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class RaceReadOnlyListFactory : IRaceReadOnlyListFactory
    {
        public async Task<IRaceReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<RaceReadOnlyList>();
            
        }

            


    }
}
