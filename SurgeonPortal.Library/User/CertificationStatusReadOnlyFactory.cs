using Csla;
using SurgeonPortal.Library.Contracts.User;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.User
{
    public class CertificationStatusReadOnlyFactory : ICertificationStatusReadOnlyFactory
    {
        public async Task<ICertificationStatusReadOnly> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<CertificationStatusReadOnly>();
            
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
