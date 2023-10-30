using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class CaseFeedbackReadOnlyFactory : ICaseFeedbackReadOnlyFactory
    {
        public async Task<ICaseFeedbackReadOnly> GetByExaminerIdAsync(int caseHeaderId)
        {
            if (caseHeaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("caseHeaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseFeedbackReadOnly>(
                new GetByExaminerIdCriteria(caseHeaderId));
            
        }


        
            [Serializable]
            internal class GetByExaminerIdCriteria
            {
                public int CaseHeaderId { get; set; }
            
                public GetByExaminerIdCriteria(int caseHeaderId)
             {
                    CaseHeaderId = caseHeaderId;
              }
            }
            


    }
}
