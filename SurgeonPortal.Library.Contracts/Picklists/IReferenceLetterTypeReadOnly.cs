using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IReferenceLetterTypeReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Role { get; }
    }
}
