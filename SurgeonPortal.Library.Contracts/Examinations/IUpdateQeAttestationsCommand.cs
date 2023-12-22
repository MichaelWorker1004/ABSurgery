using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IUpdateQeAttestationsCommand : IYtgCommandBase<int>
    {
        int UserId { get; }
        int ExamId { get; }
    }
}
