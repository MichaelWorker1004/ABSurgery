using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface ISubmitAttestationsCommand : IYtgCommandBase<int>
    {
        int UserId { get; }
        DateTime SigReceive { get; }
        DateTime CertnoticeReceive { get; }
    }
}
