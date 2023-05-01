using Csla;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations.GQ
{
    public class AdditionalTrainingReadOnlyListFactory : IAdditionalTrainingReadOnlyListFactory
    {
        public async Task<IAdditionalTrainingReadOnlyList> GetAllByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new FactoryInvalidCriteriaException("userId is a required field.");
            }
            
            return await DataPortal.FetchAsync<AdditionalTrainingReadOnlyList>(
                new GetAllByUserIdCriteria(userId));
            
        }

            [Serializable]
            internal class GetAllByUserIdCriteria
            {
                public int UserId { get; set; }
            
                public GetAllByUserIdCriteria(int userId)
             {
                    UserId = userId;
              }
            }
            


    }
}
