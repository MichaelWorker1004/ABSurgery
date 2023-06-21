using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IUserAppointmentDal
    {
        Task DeleteAsync(UserAppointmentDto dto);
        Task<UserAppointmentDto> GetByIdAsync(int apptId);
        Task<UserAppointmentDto> InsertAsync(UserAppointmentDto dto);
        Task<UserAppointmentDto> UpdateAsync(UserAppointmentDto dto);
    }
}
