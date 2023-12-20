using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class PdReferenceLetterFactory : IPdReferenceLetterFactory
    {
        public async Task<IPdReferenceLetter> GetByExamIdAsync(int examId)
        {
            if (examId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examId is a required field.");
            }
            
            return await DataPortal.FetchAsync<PdReferenceLetter>(
                new GetByExamIdCriteria(examId));
            
        }

        public IPdReferenceLetter Create()
        {
            return DataPortal.Create<PdReferenceLetter>();
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
