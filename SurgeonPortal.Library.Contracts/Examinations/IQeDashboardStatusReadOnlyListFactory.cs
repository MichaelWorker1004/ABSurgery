using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQeDashboardStatusReadOnlyListFactory
    {
        Task<IQeDashboardStatusReadOnlyList> GetByExamIdAsync(int examheaderId);
    }
}
