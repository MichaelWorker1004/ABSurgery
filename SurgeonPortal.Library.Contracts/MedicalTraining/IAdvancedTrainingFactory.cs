using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IAdvancedTrainingFactory
    {
        Task<IAdvancedTraining> GetByTrainingIdAsync(System.Collections.Generic.List`1[System.String]);
        IAdvancedTraining Create();
    }
}
