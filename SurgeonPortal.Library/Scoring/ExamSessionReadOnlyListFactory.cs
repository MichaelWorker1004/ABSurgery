using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class ExamSessionReadOnlyListFactory : IExamSessionReadOnlyListFactory
    {
        public async Task<IExamSessionReadOnlyList> GetByUserIdAsync(DateTime examDate)
        {
            
            return await DataPortal.FetchAsync<ExamSessionReadOnlyList>(
                new GetByUserIdCriteria(examDate));
            
        }

            [Serializable]
            internal class GetByUserIdCriteria
            {
                public DateTime ExamDate { get; set; }
            
                public GetByUserIdCriteria(DateTime examDate)
             {
                    ExamDate = examDate;
              }
            }
            


    }
}
