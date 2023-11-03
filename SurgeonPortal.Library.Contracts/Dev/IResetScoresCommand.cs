using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Dev
{
    public interface IResetScoresCommand : IYtgCommandBase<int>
    {
        int ExaminerUserId { get; }
    }
}
