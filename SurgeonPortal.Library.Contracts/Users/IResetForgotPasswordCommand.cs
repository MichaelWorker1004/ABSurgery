using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IResetForgotPasswordCommand : IYtgCommandBase<int>
    {
        Guid ResetGUID { get; }
        string NewPassword { get; }
        bool? Result { get; }
    }
}
