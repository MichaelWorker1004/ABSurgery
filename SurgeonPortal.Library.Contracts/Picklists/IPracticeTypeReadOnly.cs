using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IPracticeTypeReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Name { get; }
    }
}
