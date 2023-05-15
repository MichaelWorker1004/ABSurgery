using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ITrainingTypeReadOnlyListFactory
    {
        Task<ITrainingTypeReadOnlyList> GetAllAsync();
    }
}
