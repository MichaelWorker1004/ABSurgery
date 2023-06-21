using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IPrimaryPracticeReadOnlyListFactory
    {
        Task<IPrimaryPracticeReadOnlyList> GetAllAsync();
    }
}
