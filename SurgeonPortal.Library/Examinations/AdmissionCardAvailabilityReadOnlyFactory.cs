using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class AdmissionCardAvailabilityReadOnlyFactory : IAdmissionCardAvailabilityReadOnlyFactory
    {
        public async Task<IAdmissionCardAvailabilityReadOnly> GetByExamIdAsync(int examID)
        {
            if (examID <= 0)
            {
                throw new FactoryInvalidCriteriaException("examID is a required field.");
            }
            
            return await DataPortal.FetchAsync<AdmissionCardAvailabilityReadOnly>(
                new GetByExamIdCriteria(examID));
            
        }


        
            [Serializable]
            internal class GetByExamIdCriteria
            {
                public int ExamID { get; set; }
            
                public GetByExamIdCriteria(int examID)
             {
                    ExamID = examID;
              }
            }
            


    }
}
