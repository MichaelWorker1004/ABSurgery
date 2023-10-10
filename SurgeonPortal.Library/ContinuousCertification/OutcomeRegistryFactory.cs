using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class OutcomeRegistryFactory : IOutcomeRegistryFactory
    {
        public async Task<IOutcomeRegistry> GetByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new FactoryInvalidCriteriaException("userId is a required field.");
            }
            
            return await DataPortal.FetchAsync<OutcomeRegistry>(
                new GetByUserIdCriteria(userId));
            
        }

        public IOutcomeRegistry Create()
        {
            return DataPortal.Create<OutcomeRegistry>();
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
