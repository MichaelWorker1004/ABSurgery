using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface ICaseScoreDal
    {
        Task DeleteAsync(CaseScoreDto dto);
        Task<CaseScoreDto> GetByIdAsync(
            int examScoringId,
            int examinerUserId);
        Task<CaseScoreDto> InsertAsync(CaseScoreDto dto);
        Task<CaseScoreDto> UpdateAsync(CaseScoreDto dto);
    }
}
