using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IRaceReadOnly : IYtgReadOnlyBase<int>
    {
        string ItemValue { get; }
        string ItemDescription { get; }
    }
}
