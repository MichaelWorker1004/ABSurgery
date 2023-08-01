using Csla;
using SurgeonPortal.Library.Contracts.Trainees;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Trainees
{
    public class RegistrationStatusReadOnlyFactory : IRegistrationStatusReadOnlyFactory
    {
        public async Task<IRegistrationStatusReadOnly> GetByExamCodeAsync(string examCode)
        {
            if (string.IsNullOrEmpty(examCode) == true)
            {
                throw new FactoryInvalidCriteriaException("examCode is a required field.");
            }
            
            return await DataPortal.FetchAsync<RegistrationStatusReadOnly>(
                new GetByExamCodeCriteria(examCode));
            
        }


        
            [Serializable]
            internal class GetByExamCodeCriteria
            {
                public string ExamCode { get; set; }
            
                public GetByExamCodeCriteria(string examCode)
             {
                    ExamCode = examCode;
              }
            }
            


    }
}
