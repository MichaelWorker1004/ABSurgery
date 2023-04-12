using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class StateReadOnlyListFactory : IStateReadOnlyListFactory
    {
        public async Task<IStateReadOnlyList> GetByCountryAsync(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode) == true)
            {
                throw new FactoryInvalidCriteriaException("countryCode is a required field.");
            }
            
            return await DataPortal.FetchAsync<StateReadOnlyList>(
                new GetByCountryCriteria(countryCode));
            
        }

            [Serializable]
            internal class GetByCountryCriteria
            {
                public string CountryCode { get; set; }
            
                public GetByCountryCriteria(string countryCode)
             {
                    CountryCode = countryCode;
              }
            }
            


    }
}
