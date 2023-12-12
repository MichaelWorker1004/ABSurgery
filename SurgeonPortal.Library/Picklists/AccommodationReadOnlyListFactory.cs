using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class AccommodationReadOnlyListFactory : IAccommodationReadOnlyListFactory
    {
        public async Task<IAccommodationReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<AccommodationReadOnlyList>();
            
        }

            


    }
}
