using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseFeedbackReadOnlyFactory
    {
        Task<ICaseFeedbackReadOnly> GetByExaminerIdAsync(int caseHeaderId);
    }
}
