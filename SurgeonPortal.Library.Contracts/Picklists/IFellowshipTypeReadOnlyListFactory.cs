using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IFellowshipTypeReadOnlyListFactory
    {
        Task<IFellowshipTypeReadOnlyList> GetAsync();
    }
}
