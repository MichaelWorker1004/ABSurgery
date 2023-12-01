using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IAttestationReadOnly : IYtgReadOnlyBase<int>
    {
        string label { get; }
        string name { get; }
        int checked { get; }
    }
}
