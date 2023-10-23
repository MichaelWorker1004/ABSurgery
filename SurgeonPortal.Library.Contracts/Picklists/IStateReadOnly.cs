using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IStateReadOnly : IYtgReadOnlyBase<int>
    {
        string Country { get; }
        string ItemValue { get; }
        string ItemDescription { get; }
    }
}
