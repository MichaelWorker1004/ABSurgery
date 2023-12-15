using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class UserCertificateReadOnlyListFactory : IUserCertificateReadOnlyListFactory
    {
        public async Task<IUserCertificateReadOnlyList> GetByUserAndTypeAsync(int certificateTypeId)
        {
            if (certificateTypeId <= 0)
            {
                throw new FactoryInvalidCriteriaException("certificateTypeId is a required field.");
            }
            
            return await DataPortal.FetchAsync<UserCertificateReadOnlyList>(
                new GetByUserAndTypeCriteria(certificateTypeId));
            
        }

        public async Task<IUserCertificateReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<UserCertificateReadOnlyList>();
            
        }

            [Serializable]
            internal class GetByUserAndTypeCriteria
            {
                public int CertificateTypeId { get; set; }
            
                public GetByUserAndTypeCriteria(int certificateTypeId)
             {
                    CertificateTypeId = certificateTypeId;
              }
            }
            

            [Serializable]
            internal class GetByUserIdCriteria
            {
            
                public GetByUserIdCriteria()
             {
              }
            }
            


    }
}
