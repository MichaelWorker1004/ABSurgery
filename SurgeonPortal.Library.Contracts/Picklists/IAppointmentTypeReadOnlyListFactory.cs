using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IAppointmentTypeReadOnlyListFactory
    {
        Task<IAppointmentTypeReadOnlyList> GetAllAsync();
    }
}
