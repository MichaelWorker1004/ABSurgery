using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface ICaseFeedbackDal
    {
        Task DeleteAsync(CaseFeedbackDto dto);
        Task<CaseFeedbackDto> GetByIdAsync(int id);
        Task<CaseFeedbackDto> InsertAsync(CaseFeedbackDto dto);
        Task<CaseFeedbackDto> UpdateAsync(CaseFeedbackDto dto);
    }
}
