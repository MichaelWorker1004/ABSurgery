using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IReferenceLetterExplainReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Explain { get; }
    }
}
