using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class UserAppointmentFactory : IUserAppointmentFactory
    {
        public async Task<IUserAppointment> GetByIdAsync(int apptId)
        {
            if (apptId <= 0)
            {
                throw new FactoryInvalidCriteriaException("apptId is a required field.");
            }
            
            return await DataPortal.FetchAsync<UserAppointment>(
                new GetByIdCriteria(apptId));
            
        }

        public IUserAppointment Create()
        {
            return DataPortal.Create<UserAppointment>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int ApptId { get; set; }
            
                public GetByIdCriteria(int apptId)
             {
                    ApptId = apptId;
              }
            }
            


    }
}
