using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IStateReadOnly : IYtgReadOnlyBase
    {
        string Country { get; }
        string ItemValue { get; }
        string ItemDescription { get; }
    }
}
