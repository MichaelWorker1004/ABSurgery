using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class RequirementsReadOnlyFactory : IRequirementsReadOnlyFactory
    {
        public async Task<IRequirementsReadOnly> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<RequirementsReadOnly>();
            
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
