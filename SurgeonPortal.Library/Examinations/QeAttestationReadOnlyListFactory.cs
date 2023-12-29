using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class QeAttestationReadOnlyListFactory : IQeAttestationReadOnlyListFactory
    {
        public async Task<IQeAttestationReadOnlyList> GetByExamIdAsync(int examId)
        {
            if (examId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examId is a required field.");
            }
            
            return await DataPortal.FetchAsync<QeAttestationReadOnlyList>(
                new GetByExamIdCriteria(examId));
            
        }

            [Serializable]
            internal class GetByExamIdCriteria
            {
                public int ExamId { get; set; }
            
                public GetByExamIdCriteria(int examId)
             {
                    ExamId = examId;
              }
            }
            


    }
}
