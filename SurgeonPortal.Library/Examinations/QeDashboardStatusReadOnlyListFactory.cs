using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class QeDashboardStatusReadOnlyListFactory : IQeDashboardStatusReadOnlyListFactory
    {
        public async Task<IQeDashboardStatusReadOnlyList> GetByExamIdAsync(int examheaderId)
        {
            if (examheaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examheaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<QeDashboardStatusReadOnlyList>(
                new GetByExamIdCriteria(examheaderId));
            
        }

            [Serializable]
            internal class GetByExamIdCriteria
            {
                public int ExamheaderId { get; set; }
            
                public GetByExamIdCriteria(int examheaderId)
             {
                    ExamheaderId = examheaderId;
              }
            }
            


    }
}
