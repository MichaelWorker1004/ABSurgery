using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IDashboardStatusReadOnly : IYtgReadOnlyBase<int>
    {
        string StatusType { get; }
        int? Status { get; }
    }
}
