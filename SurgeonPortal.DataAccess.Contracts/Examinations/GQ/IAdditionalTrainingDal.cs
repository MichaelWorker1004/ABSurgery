using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations.GQ
{
    public interface IAdditionalTrainingDal
    {
        Task<AdditionalTrainingDto> GetByTrainingIdAsync(int trainingId);
        Task<AdditionalTrainingDto> InsertAsync(AdditionalTrainingDto dto);
        Task<AdditionalTrainingDto> UpdateAsync(AdditionalTrainingDto dto);
    }
}
