using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamHistoryReadOnlyListFactory
    {
        Task<IExamHistoryReadOnlyList> GetByUserIdAsync();
    }
}
