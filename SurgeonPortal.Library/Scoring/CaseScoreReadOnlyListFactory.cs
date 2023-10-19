using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class CaseScoreReadOnlyListFactory : ICaseScoreReadOnlyListFactory
    {
        public async Task<ICaseScoreReadOnlyList> GetByExamScheduleIdAsync(int examScheduleId)
        {
            if (examScheduleId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examScheduleId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseScoreReadOnlyList>(
                new GetByExamScheduleIdCriteria(examScheduleId));
            
        }

            [Serializable]
            internal class GetByExamScheduleIdCriteria
            {
                public int ExaminerUserId { get; set; }
                public int ExamScheduleId { get; set; }
            
                public GetByExamScheduleIdCriteria(
                int examinerUserId,
                int examScheduleId)
             {
                    ExaminerUserId = examinerUserId;
                    ExamScheduleId = examScheduleId;
              }
            }
            


    }
}
