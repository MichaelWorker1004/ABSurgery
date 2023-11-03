using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ICountryReadOnly : IYtgReadOnlyBase<int>
    {
        string ItemValue { get; }
        string ItemDescription { get; }
    }
}
