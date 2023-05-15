using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class UserTokenFactory : IUserTokenFactory
    {
        public async Task<IUserToken> GetActiveAsync(string token)
        {
            if (string.IsNullOrEmpty(token) == true)
            {
                throw new FactoryInvalidCriteriaException("token is a required field.");
            }
            
            return await DataPortal.FetchAsync<UserToken>(
                new GetActiveCriteria(token));
            
        }

        public IUserToken Create()
        {
            return DataPortal.Create<UserToken>();
        }


        
            [Serializable]
            internal class GetActiveCriteria
            {
                public string Token { get; set; }
            
                public GetActiveCriteria(string token)
             {
                    Token = token;
              }
            }
            


    }
}
