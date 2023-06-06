using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations.GQ
{
    public interface IAdditionalTrainingReadOnlyListFactory
    {
        Task<IAdditionalTrainingReadOnlyList> GetAllByUserIdAsync();
    }
}
