using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IDegreeReadOnlyListFactory
    {
        Task<IDegreeReadOnlyList> GetAllAsync();
    }
}
