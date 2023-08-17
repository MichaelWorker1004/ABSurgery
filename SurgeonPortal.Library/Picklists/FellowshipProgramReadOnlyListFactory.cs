using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class FellowshipProgramReadOnlyListFactory : IFellowshipProgramReadOnlyListFactory
    {
        public async Task<IFellowshipProgramReadOnlyList> GetAllAsync(string fellowshipType)
        {
            if (string.IsNullOrEmpty(fellowshipType) == true)
            {
                throw new FactoryInvalidCriteriaException("fellowshipType is a required field.");
            }
            
            return await DataPortal.FetchAsync<FellowshipProgramReadOnlyList>(
                new GetAllCriteria(fellowshipType));
            
        }

            [Serializable]
            internal class GetAllCriteria
            {
                public string FellowshipType { get; set; }
            
                public GetAllCriteria(string fellowshipType)
             {
                    FellowshipType = fellowshipType;
              }
            }
            


    }
}
