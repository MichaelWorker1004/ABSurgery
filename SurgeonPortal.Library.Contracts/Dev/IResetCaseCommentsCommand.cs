using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Dev
{
    public interface IResetCaseCommentsCommand : IYtgCommandBase<int>
    {
        int ExaminerUserId { get; }
    }
}
