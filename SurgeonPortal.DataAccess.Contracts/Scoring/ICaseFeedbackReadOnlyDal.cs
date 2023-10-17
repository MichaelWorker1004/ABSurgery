using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface ICaseFeedbackReadOnlyDal
    {
        Task<CaseFeedbackReadOnlyDto> GetByExaminerIdAsync(int caseHeaderId);
    }
}
