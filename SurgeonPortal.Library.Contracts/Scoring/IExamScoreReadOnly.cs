using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamScoreReadOnly : IYtgReadOnlyBase
    {
        int? DayNumber { get; }
        int? SessionNumber { get; }
        string Roster { get; }
        string DisplayName { get; }
        string Score { get; }
        string CriticalFail { get; }
        string Submitted { get; }
    }
}
