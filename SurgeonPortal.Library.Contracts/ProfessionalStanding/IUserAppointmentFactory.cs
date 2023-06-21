using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IUserAppointmentFactory
    {
        Task<IUserAppointment> GetByIdAsync(int apptId);
        IUserAppointment Create();
    }
}
