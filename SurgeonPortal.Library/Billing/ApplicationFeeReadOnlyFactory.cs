using Csla;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Billing
{
    public class ApplicationFeeReadOnlyFactory : IApplicationFeeReadOnlyFactory
    {
        public async Task<IApplicationFeeReadOnly> GetByExamIdAsync(int examId)
        {
            if (examId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examId is a required field.");
            }
            
            return await DataPortal.FetchAsync<ApplicationFeeReadOnly>(
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
