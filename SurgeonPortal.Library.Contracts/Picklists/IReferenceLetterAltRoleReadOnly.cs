using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IReferenceLetterAltRoleReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Role { get; }
    }
}
