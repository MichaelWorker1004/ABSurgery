using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IClinicalActivityReadOnlyListFactory
    {
        Task<IClinicalActivityReadOnlyList> GetAllAsync();
    }
}
