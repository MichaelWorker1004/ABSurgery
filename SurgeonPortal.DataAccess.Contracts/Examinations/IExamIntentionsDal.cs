using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IExamIntentionsDal
    {
        Task<ExamIntentionsDto> GetByExamIdAsync(
            int userId,
            int examId);
        Task<ExamIntentionsDto> InsertAsync(ExamIntentionsDto dto);
    }
}
