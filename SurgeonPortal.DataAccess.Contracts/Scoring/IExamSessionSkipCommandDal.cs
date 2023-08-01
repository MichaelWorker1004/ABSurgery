using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IExamSessionSkipCommandDal
    {
        Task SkipExamSessionAsync(int examScheduleId);
    }
}
