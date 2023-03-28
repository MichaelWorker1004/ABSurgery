using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserLoginAuditCommand : IYtgCommandBase
    {
        int UserId { get; }
        string EmailAddress { get; }
        int ApplicationId { get; }
        string LoginIpAddress { get; }
        string LoginUserAgent { get; }
        bool LoginSuccess { get; }
        string LoginFailureReason { get; }
    }
}
