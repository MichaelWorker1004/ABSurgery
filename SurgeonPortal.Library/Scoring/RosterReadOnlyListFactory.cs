using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class RosterReadOnlyListFactory : IRosterReadOnlyListFactory
    {
        public async Task<IRosterReadOnlyList> GetByExaminationHeaderIdAsync(int examHeaderId)
        {
            if (examHeaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examHeaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<RosterReadOnlyList>(
                new GetByExaminationHeaderIdCriteria(examHeaderId));
            
        }

            [Serializable]
            internal class GetByExaminationHeaderIdCriteria
            {
                public int ExamHeaderId { get; set; }
            
                public GetByExaminationHeaderIdCriteria(int examHeaderId)
             {
                    ExamHeaderId = examHeaderId;
              }
            }
            


    }
}
