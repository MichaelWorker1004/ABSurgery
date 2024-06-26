using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IPasswordValidationCommand : IYtgCommandBase<int>
    {
        bool? PasswordsMatch { get; }
        string Password { get; }
        int UserId { get; }
    }
}
