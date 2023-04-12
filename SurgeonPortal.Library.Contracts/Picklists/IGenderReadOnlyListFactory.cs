using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IGenderReadOnlyListFactory
    {
        Task<IGenderReadOnlyList> GetAllAsync();
    }
}
