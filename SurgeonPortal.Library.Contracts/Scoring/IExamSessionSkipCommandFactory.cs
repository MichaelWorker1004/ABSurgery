using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamSessionSkipCommandFactory
    {
        Task<IExamSessionSkipCommand> SkipExamSessionAsync(int examScheduleId);
    }
}
