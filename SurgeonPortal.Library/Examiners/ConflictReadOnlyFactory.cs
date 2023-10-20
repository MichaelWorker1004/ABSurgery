using Csla;
using SurgeonPortal.Library.Contracts.Examiners;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examiners
{
    public class ConflictReadOnlyFactory : IConflictReadOnlyFactory
    {
        public async Task<IConflictReadOnly> GetByExamHeaderIdAsync(int examHeaderId)
        {
            if (examHeaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examHeaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<ConflictReadOnly>(
                new GetByExamHeaderIdCriteria(examHeaderId));
            
        }


        
            [Serializable]
            internal class GetByExamHeaderIdCriteria
            {
                public int ExamHeaderId { get; set; }
            
                public GetByExamHeaderIdCriteria(int examHeaderId)
             {
                    ExamHeaderId = examHeaderId;
              }
            }
            


    }
}
