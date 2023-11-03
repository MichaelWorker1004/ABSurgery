using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class UserCertificateReadOnlyListFactory : IUserCertificateReadOnlyListFactory
    {
        public async Task<IUserCertificateReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<UserCertificateReadOnlyList>();
            
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
