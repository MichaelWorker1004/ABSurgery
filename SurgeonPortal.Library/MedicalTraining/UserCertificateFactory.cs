using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class UserCertificateFactory : IUserCertificateFactory
    {
        public async Task<IUserCertificate> GetByIdAsync(int certificateId)
        {
            if (certificateId <= 0)
            {
                throw new FactoryInvalidCriteriaException("certificateId is a required field.");
            }
            
            return await DataPortal.FetchAsync<UserCertificate>(
                new GetByIdCriteria(certificateId));
            
        }

        public IUserCertificate Create()
        {
            return DataPortal.Create<UserCertificate>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int CertificateId { get; set; }
            
                public GetByIdCriteria(int certificateId)
             {
                    CertificateId = certificateId;
              }
            }
            


    }
}
