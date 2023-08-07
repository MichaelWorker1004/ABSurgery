using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamOverviewReadOnlyListFactory
    {
        Task<IExamOverviewReadOnlyList> GetAllAsync();
    }
}
