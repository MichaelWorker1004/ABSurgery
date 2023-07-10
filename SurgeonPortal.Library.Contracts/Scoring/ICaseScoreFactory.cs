using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseScoreFactory
    {
        Task<ICaseScore> GetByIdAsync(int examScoringId);
        ICaseScore Create();
    }
}
