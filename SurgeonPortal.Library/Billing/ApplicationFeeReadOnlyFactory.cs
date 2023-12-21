using Csla;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Billing
{
    public class ApplicationFeeReadOnlyFactory : IApplicationFeeReadOnlyFactory
    {
        public async Task<IApplicationFeeReadOnly> GetByExamIdAsync(
            int userId,
            int examId)
        {
            if (userId <= 0)
            {
                throw new FactoryInvalidCriteriaException("userId is a required field.");
            }
            if (examId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examId is a required field.");
            }
            
            return await DataPortal.FetchAsync<ApplicationFeeReadOnly>(
                new GetByExamIdCriteria(
                userId,
                examId));
            
        }


        
            [Serializable]
            internal class GetByExamIdCriteria
            {
                public int UserId { get; set; }
                public int ExamId { get; set; }
            
                public GetByExamIdCriteria(
                int userId,
                int examId)
             {
                    UserId = userId;
                    ExamId = examId;
              }
            }
            


    }
}
