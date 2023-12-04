using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class AttestationReadOnlyListFactory : IAttestationReadOnlyListFactory
    {
        public async Task<IAttestationReadOnlyList> GetAllByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<AttestationReadOnlyList>();
            
        }

            [Serializable]
            internal class GetAllByUserIdCriteria
            {
            
                public GetAllByUserIdCriteria()
             {
              }
            }
            


    }
}
