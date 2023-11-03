using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IOrganizationTypeReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Type { get; }
    }
}
