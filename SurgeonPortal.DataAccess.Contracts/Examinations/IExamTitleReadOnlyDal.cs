using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IExamTitleReadOnlyDal
    {
        Task<ExamTitleReadOnlyDto> GetByExamIdAsync(int examId);
    }
}
