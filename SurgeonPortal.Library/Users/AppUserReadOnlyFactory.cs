using Csla;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Users
{
    public class AppUserReadOnlyFactory : IAppUserReadOnlyFactory
    {
        public async Task<IAppUserReadOnly> GetByCredentialsAsync(
            string emailAddress,
            string password)
        {
            if (string.IsNullOrEmpty(emailAddress) == true)
            {
                throw new FactoryInvalidCriteriaException("emailAddress is a required field.");
            }
            if (string.IsNullOrEmpty(password) == true)
            {
                throw new FactoryInvalidCriteriaException("password is a required field.");
            }
            
            return await DataPortal.FetchAsync<AppUserReadOnly>(new GetByCredentialsCriteria(
                emailAddress,
                password));
        }

        public async Task<IAppUserReadOnly> GetByTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token) == true)
            {
                throw new FactoryInvalidCriteriaException("token is a required field.");
            }
            
            return await DataPortal.FetchAsync<AppUserReadOnly>(new GetByTokenCriteria(token));
        }


        
        [Serializable]
        internal class GetByCredentialsCriteria
        {
            public string EmailAddress { get; set; }
            public string Password { get; set; }
        
            public GetByCredentialsCriteria(
                string emailAddress,
                string password)
            {
                EmailAddress = emailAddress;
                Password = password;
            }
        }

        [Serializable]
        internal class GetByTokenCriteria
        {
            public string Token { get; set; }
        
            public GetByTokenCriteria(string token)
            {
                Token = token;
            }
        }


    }
}
