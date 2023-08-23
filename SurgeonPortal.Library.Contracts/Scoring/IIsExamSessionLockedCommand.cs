using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IIsExamSessionLockedCommand : IYtgCommandBase
    {
        int ExamCaseId { get; }
        bool? IsLocked { get; }
    }
}
