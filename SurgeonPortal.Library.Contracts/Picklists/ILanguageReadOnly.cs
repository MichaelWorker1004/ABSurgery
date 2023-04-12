using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ILanguageReadOnly : IYtgReadOnlyBase
    {
        int ItemValue { get; }
        string ItemDescription { get; }
    }
}
