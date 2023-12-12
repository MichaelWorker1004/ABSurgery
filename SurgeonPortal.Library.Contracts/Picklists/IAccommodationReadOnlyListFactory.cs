using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IAccommodationReadOnlyListFactory
    {
        Task<IAccommodationReadOnlyList> GetAllAsync();
    }
}
