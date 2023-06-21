using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class AppointmentTypeReadOnlyListFactory : IAppointmentTypeReadOnlyListFactory
    {
        public async Task<IAppointmentTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<AppointmentTypeReadOnlyList>();
            
        }

            


    }
}
