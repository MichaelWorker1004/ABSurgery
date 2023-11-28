using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IDashboardStatusReadOnlyListFactory
    {
        Task<IDashboardStatusReadOnlyList> GetAllByUserIdAsync();
    }
}
