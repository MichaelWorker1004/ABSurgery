using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IRaceReadOnlyListFactory
    {
        Task<IRaceReadOnlyList> GetAllAsync();
    }
}
