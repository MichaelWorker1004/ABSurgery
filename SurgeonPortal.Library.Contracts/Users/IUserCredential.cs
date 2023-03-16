using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserCredential : IYtgBusinessBase
    {
        int UserId { get; set; }
        string EmailAddress { get; set; }
        string Password { get; set; }
    }
}
