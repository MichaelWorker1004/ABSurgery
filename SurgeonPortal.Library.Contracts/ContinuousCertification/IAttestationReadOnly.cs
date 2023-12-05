using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IAttestationReadOnly : IYtgReadOnlyBase<int>
    {
        string Label { get; }
        string Name { get; }
        int Checked { get; }
    }
}
