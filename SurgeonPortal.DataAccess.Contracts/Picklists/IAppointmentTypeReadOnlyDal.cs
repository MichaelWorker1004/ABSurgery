using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IAppointmentTypeReadOnlyDal
    {
        Task<IEnumerable<AppointmentTypeReadOnlyDto>> GetAllAsync();
    }
}
