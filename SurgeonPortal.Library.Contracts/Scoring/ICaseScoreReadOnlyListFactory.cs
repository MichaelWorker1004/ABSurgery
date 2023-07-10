using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseScoreReadOnlyListFactory
    {
        Task<ICaseScoreReadOnlyList> GetByExamScheduleIdAsync(int examScheduleId);
    }
}
