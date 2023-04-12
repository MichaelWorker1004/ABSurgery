using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IStateReadOnlyListFactory
    {
        Task<IStateReadOnlyList> GetByCountryAsync(string countryCode);
    }
}
