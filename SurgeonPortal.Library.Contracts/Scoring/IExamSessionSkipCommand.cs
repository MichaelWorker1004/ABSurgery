using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamSessionSkipCommand : IYtgCommandBase<int>
    {
        int ExamScheduleId { get; }
        int ExaminerUserId { get; }
    }
}
