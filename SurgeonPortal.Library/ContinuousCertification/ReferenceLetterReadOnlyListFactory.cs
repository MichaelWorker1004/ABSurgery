using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class ReferenceLetterReadOnlyListFactory : IReferenceLetterReadOnlyListFactory
    {
        public async Task<IReferenceLetterReadOnlyList> GetAllByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<ReferenceLetterReadOnlyList>();
            
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
