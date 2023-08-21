using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseFeedbackFactory
    {
        Task<ICaseFeedback> GetByIdAsync(int id);
        ICaseFeedback Create();
    }
}
