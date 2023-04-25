using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class OutcomeRegistryFactory : IOutcomeRegistryFactory
    {
        public async Task<IOutcomeRegistry> GetByUserIdAsync(int userId)
        {
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
