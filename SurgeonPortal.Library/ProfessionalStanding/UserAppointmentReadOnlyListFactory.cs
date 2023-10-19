using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class UserAppointmentReadOnlyListFactory : IUserAppointmentReadOnlyListFactory
    {
        public async Task<IUserAppointmentReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<UserAppointmentReadOnlyList>();
            
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
