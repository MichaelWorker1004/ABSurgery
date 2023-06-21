using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IUserAppointmentReadOnlyDal
    {
        Task<IEnumerable<UserAppointmentReadOnlyDto>> GetByUserIdAsync();
    }
}
