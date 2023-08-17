using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IFellowshipProgramReadOnlyListFactory
    {
        Task<IFellowshipProgramReadOnlyList> GetAllAsync(string fellowshipType);
    }
}
