using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IVerifyForgotGuidCommand : IYtgCommandBase<int>
    {
        Guid ResetGUID { get; }
        bool Result { get; }
    }
}
