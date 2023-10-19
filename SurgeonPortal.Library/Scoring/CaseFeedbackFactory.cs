using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class CaseFeedbackFactory : ICaseFeedbackFactory
    {
        public async Task<ICaseFeedback> GetByExaminerIdAsync(int caseHeaderId)
        {
            if (caseHeaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("caseHeaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseFeedback>(
                new GetByExaminerIdCriteria(caseHeaderId));
            
        }

        public async Task<ICaseFeedback> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new FactoryInvalidCriteriaException("id is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseFeedback>(
                new GetByIdCriteria(id));
            
        }

        public ICaseFeedback Create()
        {
            return DataPortal.Create<CaseFeedback>();
        }


        
            [Serializable]
            internal class GetByExaminerIdCriteria
            {
                public int ExaminerUserId { get; set; }
                public int CaseHeaderId { get; set; }
            
                public GetByExaminerIdCriteria(
                int examinerUserId,
                int caseHeaderId)
             {
                    ExaminerUserId = examinerUserId;
                    CaseHeaderId = caseHeaderId;
              }
            }
            

            [Serializable]
            internal class GetByIdCriteria
            {
                public int Id { get; set; }
            
                public GetByIdCriteria(int id)
             {
                    Id = id;
              }
            }
            


    }
}
