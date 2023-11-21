using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IAttestationStatusReadOnly : IYtgReadOnlyBase<int>
    {
        int Status { get; }
    }
}
