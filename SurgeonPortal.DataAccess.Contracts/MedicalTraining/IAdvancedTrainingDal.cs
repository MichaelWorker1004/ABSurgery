using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IAdvancedTrainingDal
    {
        Task<AdvancedTrainingDto> GetByTrainingIdAsync(int id);
        Task<AdvancedTrainingDto> InsertAsync(AdvancedTrainingDto dto);
        Task<AdvancedTrainingDto> UpdateAsync(AdvancedTrainingDto dto);
    }
}
