using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserToken : IYtgBusinessBase
    {
        int UserId { get; set; }
        string Token { get; set; }
        DateTime ExpiresAt { get; set; }
    }
}
