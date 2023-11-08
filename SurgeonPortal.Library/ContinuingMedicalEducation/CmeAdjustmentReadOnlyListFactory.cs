using Csla;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    public class CmeAdjustmentReadOnlyListFactory : ICmeAdjustmentReadOnlyListFactory
    {
        public async Task<ICmeAdjustmentReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<CmeAdjustmentReadOnlyList>();
            
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
