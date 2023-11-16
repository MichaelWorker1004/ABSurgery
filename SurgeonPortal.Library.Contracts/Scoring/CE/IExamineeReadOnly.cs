using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring.CE
{
    public interface IExamineeReadOnly : IYtgReadOnlyBase<int>
    {
        int ExamScheduleId { get; }
        string FullName { get; }
        string ExamDate { get; }
        ITitleReadOnlyList Cases { get; }
        int ExamineeUserId { get; }
        int ExamScoringId { get; }
        bool? TimerBit { get; }
    }
}
