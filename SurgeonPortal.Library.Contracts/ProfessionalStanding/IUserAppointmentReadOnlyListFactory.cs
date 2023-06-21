using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IUserAppointmentReadOnlyListFactory
    {
        Task<IUserAppointmentReadOnlyList> GetByUserIdAsync();
    }
}
