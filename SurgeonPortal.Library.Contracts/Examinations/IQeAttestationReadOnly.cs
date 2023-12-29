using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQeAttestationReadOnly : IYtgReadOnlyBase<int>
    {
        string AttestationText { get; }
        string Name { get; }
        int Checked { get; }
    }
}
