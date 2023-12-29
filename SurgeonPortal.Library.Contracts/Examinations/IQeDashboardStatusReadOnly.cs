using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQeDashboardStatusReadOnly : IYtgReadOnlyBase<int>
    {
        string StatusType { get; }
        int? Status { get; }
        int Disabled { get; }
    }
}
