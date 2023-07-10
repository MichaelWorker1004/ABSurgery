using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseRosterReadOnlyListFactory
    {
        Task<ICaseRosterReadOnlyList> GetByScheduleIdAsync(
            int scheduleId1,
            int? scheduleId2);
    }
}
