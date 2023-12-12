using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class AccommodationFactory : IAccommodationFactory
    {
        public async Task<IAccommodation> GetByExamIdAsync(int examId)
        {
            if (examId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examId is a required field.");
            }
            
            return await DataPortal.FetchAsync<Accommodation>(
                new GetByExamIdCriteria(examId));
            
        }

        public IAccommodation Create()
        {
            return DataPortal.Create<Accommodation>();
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
