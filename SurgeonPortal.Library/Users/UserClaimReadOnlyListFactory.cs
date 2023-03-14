using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class UserClaimReadOnlyListFactory : IUserClaimReadOnlyListFactory
    {
        public async Task<IUserClaimReadOnlyList> GetByIdsAsync(
            int userId,
            int applicationId)
        {
            if (userId <= 0)
            {
                throw new FactoryInvalidCriteriaException("userId is a required field.");
            }
            if (applicationId <= 0)
            {
                throw new FactoryInvalidCriteriaException("applicationId is a required field.");
            }
            
            return await DataPortal.FetchAsync<UserClaimReadOnlyList>(
                new GetByIdsCriteria(
                    userId,
                    applicationId));
        }

        [Serializable]
        internal class GetByIdsCriteria
        {
            public int UserId { get; set; }
            public int ApplicationId { get; set; }
        
            public GetByIdsCriteria(
                int userId,
                int applicationId)
            {
                UserId = userId;
                ApplicationId = applicationId;
            }
        }


    }
}
