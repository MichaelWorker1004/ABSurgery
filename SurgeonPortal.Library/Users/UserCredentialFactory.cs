using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Users
{
    public class UserCredentialFactory : IUserCredentialFactory
    {
        public async Task<IUserCredential> GetByUserIdAsync()
        {
            return await DataPortal.FetchAsync<UserCredential>(new GetByUserIdCriteria());
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
