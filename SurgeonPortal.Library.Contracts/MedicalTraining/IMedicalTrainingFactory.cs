using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IMedicalTrainingFactory
    {
        Task<IMedicalTraining> GetByUserIdAsync();
        IMedicalTraining Create();
    }
}
