using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IForgotUsernameCommand : IYtgCommandBase<int>
    {
        string Email { get; }
        string UserName { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}
