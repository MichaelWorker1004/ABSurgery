using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IClinicalLevelReadOnlyListFactory
    {
        Task<IClinicalLevelReadOnlyList> GetAllAsync();
    }
}
