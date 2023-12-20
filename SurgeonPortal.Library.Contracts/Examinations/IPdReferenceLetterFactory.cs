using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IPdReferenceLetterFactory
    {
        Task<IPdReferenceLetter> GetByExamIdAsync(int examId);
        IPdReferenceLetter Create();
    }
}
