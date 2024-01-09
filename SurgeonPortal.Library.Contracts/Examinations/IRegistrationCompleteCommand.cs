using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IRegistrationCompleteCommand : IYtgCommandBase<int>
    {
        int UserId { get; }
        int ExamHeaderId { get; }
    }
}
