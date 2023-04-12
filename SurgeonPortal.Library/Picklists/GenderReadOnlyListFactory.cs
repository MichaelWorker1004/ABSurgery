using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class GenderReadOnlyListFactory : IGenderReadOnlyListFactory
    {
        public async Task<IGenderReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<GenderReadOnlyList>();
            
        }

            


    }
}
