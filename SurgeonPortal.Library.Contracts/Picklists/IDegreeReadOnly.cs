using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IDegreeReadOnly : IYtgReadOnlyBase<int>
    {
        string ItemDisplay { get; }
        int ItemValue { get; }
    }
}
