using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQeExamEligibilityReadOnlyListFactory
    {
        Task<IQeExamEligibilityReadOnlyList> GetByUserIdAsync();
    }
}
