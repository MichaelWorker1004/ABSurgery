using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Users
{
    public class UserCredentialFactory : IUserCredentialFactory
    {
        public async Task<IUserCredential> GetByUserIdAsync(int userId)
        {
            return await DataPortal.FetchAsync<UserCredential>(new GetByUserIdCriteria(userId));
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
