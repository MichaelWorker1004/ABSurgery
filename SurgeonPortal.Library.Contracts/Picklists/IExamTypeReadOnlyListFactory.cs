using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IExamTypeReadOnlyListFactory
    {
        Task<IExamTypeReadOnlyList> GetAllAsync();
    }
}
