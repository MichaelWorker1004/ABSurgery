using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class CaseScoreFactory : ICaseScoreFactory
    {
        public async Task<ICaseScore> GetByIdAsync(int examScoringId)
        {
            if (examScoringId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examScoringId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseScore>(
                new GetByIdCriteria(examScoringId));
            
        }

        public ICaseScore Create()
        {
            return DataPortal.Create<CaseScore>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int ExamScoringId { get; set; }
                public int ExaminerUserId { get; set; }
            
                public GetByIdCriteria(
                int examScoringId,
                int examinerUserId)
             {
                    ExamScoringId = examScoringId;
                    ExaminerUserId = examinerUserId;
              }
            }
            


    }
}
