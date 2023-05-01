using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IAppUserReadOnly : IYtgReadOnlyBase
    {
        int UserId { get; }
        string FullName { get; }
        string UserName { get; }
        string EmailAddress { get; }
        DateTime? LastLoginDateUtc { get; }
        IUserClaimReadOnlyList Claims { get; }
    }
}
