using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IAdvancedTrainingReadOnlyListFactory
    {
        Task<IAdvancedTrainingReadOnlyList> GetByUserIdAsync();
    }
}
