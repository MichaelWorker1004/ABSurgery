using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IRosterReadOnly : IYtgReadOnlyBase<int>
    {
        int ExamScheduleId { get; }
        int? DayNumber { get; }
        int? SessionNumber { get; }
        string Roster { get; }
        string DisplayName { get; }
        bool? IsSubmitted { get; }
    }
}
