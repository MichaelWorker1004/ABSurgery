using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IPasswordResetCommand : IYtgCommandBase<int>
    {
        int UserId { get; }
        string OldPassword { get; }
        string NewPassword { get; }
        bool? PasswordReset { get; }
    }
}
