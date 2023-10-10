using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IMedicalTrainingDal
    {
        Task<MedicalTrainingDto> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<MedicalTrainingDto> InsertAsync(MedicalTrainingDto dto);
        Task<MedicalTrainingDto> UpdateAsync(MedicalTrainingDto dto);
    }
}
