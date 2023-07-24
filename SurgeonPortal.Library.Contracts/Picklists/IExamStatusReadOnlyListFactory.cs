using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IExamStatusReadOnlyListFactory
    {
        Task<IExamStatusReadOnlyList> GetAllAsync();
    }
}
