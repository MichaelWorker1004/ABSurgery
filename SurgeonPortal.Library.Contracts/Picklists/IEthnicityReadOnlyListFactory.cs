using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IEthnicityReadOnlyListFactory
    {
        Task<IEthnicityReadOnlyList> GetAllAsync();
    }
}
