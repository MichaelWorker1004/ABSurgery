using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ScoringSessionReadOnlyListFactory : IScoringSessionReadOnlyListFactory
    {
        public async Task<IScoringSessionReadOnlyList> GetByExaminerIdAsync(int examHeaderId)
        {
            if (examHeaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examHeaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<ScoringSessionReadOnlyList>(
                new GetByExaminerIdCriteria(examHeaderId));
            
        }

            [Serializable]
            internal class GetByExaminerIdCriteria
            {
                public int ExamHeaderId { get; set; }
            
                public GetByExaminerIdCriteria(int examHeaderId)
             {
                    ExamHeaderId = examHeaderId;
              }
            }
            


    }
}
