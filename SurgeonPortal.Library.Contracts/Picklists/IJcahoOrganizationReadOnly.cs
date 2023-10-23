using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IJcahoOrganizationReadOnly : IYtgReadOnlyBase<int>
    {
        int? OrganizationId { get; }
        string OrganizationName { get; }
    }
}
