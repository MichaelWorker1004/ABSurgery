using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserClaimReadOnly : IYtgReadOnlyBase
    {
        string ClaimName { get; }
        int ApplicationId { get; }
        string ApplicationName { get; }
        int UserClaimId { get; }
        int ApplicationClaimId { get; }
        int UserId { get; }
    }
}
