using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IInsertUserExamCommand : IYtgCommandBase<int>
    {
        int UserId { get; }
        int ExamHeaderId { get; }
    }
}
