using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ICertificateTypeReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string Name { get; }
    }
}
