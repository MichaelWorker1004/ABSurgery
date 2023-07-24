using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IExamSpecialtyReadOnlyListFactory
    {
        Task<IExamSpecialtyReadOnlyList> GetAllAsync();
    }
}
