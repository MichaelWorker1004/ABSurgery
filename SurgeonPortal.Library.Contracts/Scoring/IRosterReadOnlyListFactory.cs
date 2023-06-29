using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IRosterReadOnlyListFactory
    {
        Task<IRosterReadOnlyList> GetByExaminationHeaderIdAsync(int examHeaderId);
    }
}
