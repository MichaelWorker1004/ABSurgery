using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IGraduateProfileReadOnlyListFactory
    {
        Task<IGraduateProfileReadOnlyList> GetAllAsync();
    }
}
