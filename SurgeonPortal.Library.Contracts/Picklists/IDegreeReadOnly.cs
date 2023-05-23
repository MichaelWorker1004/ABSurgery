using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IDegreeReadOnly : IYtgReadOnlyBase
    {
        string ItemDisplay { get; }
        int ItemValue { get; }
    }
}
