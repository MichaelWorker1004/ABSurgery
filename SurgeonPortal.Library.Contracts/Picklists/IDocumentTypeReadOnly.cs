using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IDocumentTypeReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string Name { get; }
    }
}
