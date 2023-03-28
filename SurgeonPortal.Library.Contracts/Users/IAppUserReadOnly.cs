using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IAppUserReadOnly : IYtgReadOnlyBase
    {
        int UserId { get; }
        string FullName { get; }
        string EmailAddress { get; }

        IUserClaimReadOnlyList Claims { get; }
    }
}
