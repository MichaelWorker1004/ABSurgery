using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IOrganizationTypeReadOnlyListFactory
    {
        Task<IOrganizationTypeReadOnlyList> GetAllAsync();
    }
}
