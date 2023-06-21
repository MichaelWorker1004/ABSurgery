using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ILicenseTypeReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string Name { get; }
    }
}
