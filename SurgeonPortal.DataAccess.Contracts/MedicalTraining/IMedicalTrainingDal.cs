using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IMedicalTrainingDal
    {
        Task<MedicalTrainingDto> GetByUserIdAsync(int userId);
        Task<MedicalTrainingDto> InsertAsync(MedicalTrainingDto dto);
        Task<MedicalTrainingDto> UpdateAsync(MedicalTrainingDto dto);
    }
}
