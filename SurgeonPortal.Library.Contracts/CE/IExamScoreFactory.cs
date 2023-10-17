using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.CE
{
    public interface IExamScoreFactory
    {
        Task<IExamScore> GetByIdAsync(int examScheduleScoreId);
        IExamScore Create();
    }
}
