using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IJcahoOrganizationReadOnlyListFactory
    {
        Task<IJcahoOrganizationReadOnlyList> GetAllAsync();
    }
}
