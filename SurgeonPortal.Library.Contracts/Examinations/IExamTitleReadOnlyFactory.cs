using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamTitleReadOnlyFactory
    {
        Task<IExamTitleReadOnly> GetByExamIdAsync(int examId);
    }
}
