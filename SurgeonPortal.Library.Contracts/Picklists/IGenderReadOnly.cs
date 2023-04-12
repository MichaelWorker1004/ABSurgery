using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IGenderReadOnly : IYtgReadOnlyBase
    {
        int ItemValue { get; }
        string ItemDescription { get; }
    }
}
