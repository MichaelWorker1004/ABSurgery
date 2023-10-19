using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class UserProfessionalStandingFactory : IUserProfessionalStandingFactory
    {
        public async Task<IUserProfessionalStanding> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<UserProfessionalStanding>();
            
        }

        public IUserProfessionalStanding Create()
        {
            return DataPortal.Create<UserProfessionalStanding>();
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
