using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IExamSessionLockCommandDal
    {
        Task LockExamSessionAsync(int examscheduleId);
    }
}
