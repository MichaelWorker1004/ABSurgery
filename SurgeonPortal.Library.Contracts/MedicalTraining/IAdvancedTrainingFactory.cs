using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IAdvancedTrainingFactory
    {
        Task<IAdvancedTraining> GetByTrainingIdAsync(int id);
        IAdvancedTraining Create();
    }
}
