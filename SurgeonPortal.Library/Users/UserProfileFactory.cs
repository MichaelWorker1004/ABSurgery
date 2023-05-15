using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class UserProfileFactory : IUserProfileFactory
    {
        public async Task<IUserProfile> GetByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new FactoryInvalidCriteriaException("userId is a required field.");
            }
            
            return await DataPortal.FetchAsync<UserProfile>(
                new GetByUserIdCriteria(userId));
            
        }

        public IUserProfile Create()
        {
            return DataPortal.Create<UserProfile>();
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
