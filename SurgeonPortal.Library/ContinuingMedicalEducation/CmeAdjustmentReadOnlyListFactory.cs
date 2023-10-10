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
            if (userId <= 0)
            {
                throw new FactoryInvalidCriteriaException("userId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CmeAdjustmentReadOnlyList>();
            
        }

            [Serializable]
            internal class GetByUserIdCriteria
            {
                public int UserId { get; set; }
            
                public GetByUserIdCriteria(int userId)
             {
                    UserId = userId;
              }
            }
            


    }
}
