using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations.GQ
{
    public interface IAdditionalTrainingFactory
    {
        Task<IAdditionalTraining> GetByTrainingIdAsync(int trainingId);
        IAdditionalTraining Create();
    }
}
