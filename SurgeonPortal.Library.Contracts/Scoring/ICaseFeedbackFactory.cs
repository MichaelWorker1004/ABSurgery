using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseFeedbackFactory
    {
        Task<ICaseFeedback> GetByExaminerIdAsync(int caseHeaderId);
        Task<ICaseFeedback> GetByIdAsync(int id);
        ICaseFeedback Create();
    }
}
