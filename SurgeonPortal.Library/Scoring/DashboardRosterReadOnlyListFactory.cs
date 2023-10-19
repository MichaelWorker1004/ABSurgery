using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class DashboardRosterReadOnlyListFactory : IDashboardRosterReadOnlyListFactory
    {
        public async Task<IDashboardRosterReadOnlyList> GetByUserIdAsync(DateTime examDate)
        {
            
            return await DataPortal.FetchAsync<DashboardRosterReadOnlyList>(
                new GetByUserIdCriteria(examDate));
            
        }

            [Serializable]
            internal class GetByUserIdCriteria
            {
                public int ExaminerUserId { get; set; }
                public DateTime ExamDate { get; set; }
            
                public GetByUserIdCriteria(
                int examinerUserId,
                DateTime examDate)
             {
                    ExaminerUserId = examinerUserId;
                    ExamDate = examDate;
              }
            }
            


    }
}
