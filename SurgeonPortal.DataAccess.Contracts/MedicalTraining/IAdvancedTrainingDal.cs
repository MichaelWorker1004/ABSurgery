using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IAdvancedTrainingDal
    {
        Task<AdvancedTrainingDto> GetByTrainingIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<AdvancedTrainingDto> InsertAsync(AdvancedTrainingDto dto);
        Task<AdvancedTrainingDto> UpdateAsync(AdvancedTrainingDto dto);
    }
}
