using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class UserCertificateFactory : IUserCertificateFactory
    {
        public async Task<IUserCertificate> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new FactoryInvalidCriteriaException("id is a required field.");
            }
            
            return await DataPortal.FetchAsync<UserCertificate>(
                new GetByIdCriteria(id));
            
        }

        public IUserCertificate Create()
        {
            return DataPortal.Create<UserCertificate>();
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
