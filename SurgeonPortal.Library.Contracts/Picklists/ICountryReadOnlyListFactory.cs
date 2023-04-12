using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ICountryReadOnlyListFactory
    {
        Task<ICountryReadOnlyList> GetAllAsync();
    }
}
