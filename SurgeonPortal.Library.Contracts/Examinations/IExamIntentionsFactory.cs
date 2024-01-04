using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamIntentionsFactory
    {
        Task<IExamIntentions> GetByExamIdAsync(int examId);
        IExamIntentions Create();
    }
}
