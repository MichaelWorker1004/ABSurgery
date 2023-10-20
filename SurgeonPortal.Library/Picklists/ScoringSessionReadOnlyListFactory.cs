using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ScoringSessionReadOnlyListFactory : IScoringSessionReadOnlyListFactory
    {
        public async Task<IScoringSessionReadOnlyList> GetByKeysAsync(DateTime currentDate)
        {
            
            return await DataPortal.FetchAsync<ScoringSessionReadOnlyList>(
                new GetByKeysCriteria(currentDate));
            
        }

            [Serializable]
            internal class GetByKeysCriteria
            {
                public DateTime CurrentDate { get; set; }
            
                public GetByKeysCriteria(DateTime currentDate)
             {
                    CurrentDate = currentDate;
              }
            }
            


    }
}
