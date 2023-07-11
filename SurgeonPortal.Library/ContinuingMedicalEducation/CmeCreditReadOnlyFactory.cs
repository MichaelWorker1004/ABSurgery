using Csla;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    public class CmeCreditReadOnlyFactory : ICmeCreditReadOnlyFactory
    {
        public async Task<ICmeCreditReadOnly> GetByIdAsync(int cmeId)
        {
            if (cmeId <= 0)
            {
                throw new FactoryInvalidCriteriaException("cmeId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CmeCreditReadOnly>(
                new GetByIdCriteria(cmeId));
            
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int CmeId { get; set; }
            
                public GetByIdCriteria(int cmeId)
             {
                    CmeId = cmeId;
              }
            }
            


    }
}
