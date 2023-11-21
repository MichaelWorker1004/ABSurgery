using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class AttestationStatusReadOnlyFactory : IAttestationStatusReadOnlyFactory
    {
        public async Task<IAttestationStatusReadOnly> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<AttestationStatusReadOnly>();
            
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
