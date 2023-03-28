using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Users
{
    public class UserProfileFactory : IUserProfileFactory
    {
        public async Task<IUserProfile> GetByUserIdAsync(int userId)
        {
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
