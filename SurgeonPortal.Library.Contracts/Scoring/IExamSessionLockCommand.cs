using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamSessionLockCommand : IYtgCommandBase<int>
    {
        int ExamscheduleId { get; }
    }
}
