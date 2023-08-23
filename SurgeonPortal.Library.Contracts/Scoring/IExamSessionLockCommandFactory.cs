using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamSessionLockCommandFactory
    {
        Task<IExamSessionLockCommand> LockExamSessionAsync(int examscheduleId);
    }
}
