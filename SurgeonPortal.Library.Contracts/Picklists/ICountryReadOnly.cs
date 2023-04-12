using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ICountryReadOnly : IYtgReadOnlyBase
    {
        string ItemValue { get; }
        string ItemDescription { get; }
    }
}
