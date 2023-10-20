using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class CaseDetailReadOnlyListFactory : ICaseDetailReadOnlyListFactory
    {
        public async Task<ICaseDetailReadOnlyList> GetByCaseHeaderIdAsync(int caseHeaderId)
        {
            if (caseHeaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("caseHeaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseDetailReadOnlyList>(
                new GetByCaseHeaderIdCriteria(caseHeaderId));
            
        }

            [Serializable]
            internal class GetByCaseHeaderIdCriteria
            {
                public int CaseHeaderId { get; set; }
            
                public GetByCaseHeaderIdCriteria(int caseHeaderId)
             {
                    CaseHeaderId = caseHeaderId;
              }
            }
            


    }
}
