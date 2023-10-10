using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IMedicalTrainingFactory
    {
        Task<IMedicalTraining> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
        IMedicalTraining Create();
    }
}
