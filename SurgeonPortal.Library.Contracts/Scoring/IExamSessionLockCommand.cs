using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamSessionLockCommand : IYtgCommandBase
    {
        int ExamscheduleId { get; }
    }
}
