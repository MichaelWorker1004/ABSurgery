using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class CountryReadOnlyListFactory : ICountryReadOnlyListFactory
    {
        public async Task<ICountryReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<CountryReadOnlyList>();
            
        }

            


    }
}
