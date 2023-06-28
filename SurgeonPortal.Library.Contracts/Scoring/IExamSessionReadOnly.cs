using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamSessionReadOnly : IYtgReadOnlyBase
    {
        string FirstName { get; }
        string LastName { get; }
        string StartTime { get; }
        string EndTime { get; }
        string MeetingLink { get; }
        bool? IsSubmitted { get; }
        bool? IsCurrentSession { get; }
        int? SessionNumber { get; }
    }
}
