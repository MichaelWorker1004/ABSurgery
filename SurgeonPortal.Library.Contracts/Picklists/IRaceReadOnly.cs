using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IRaceReadOnly : IYtgReadOnlyBase
    {
        string ItemValue { get; }
        string ItemDescription { get; }
    }
}
