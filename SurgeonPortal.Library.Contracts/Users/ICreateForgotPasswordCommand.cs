using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface ICreateForgotPasswordCommand : IYtgCommandBase<int>
    {
        string UserName { get; }
        Guid? ResetGUID { get; }
        string EmailAddress { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}
