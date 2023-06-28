using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseDetailReadOnlyListFactory
    {
        Task<ICaseDetailReadOnlyList> GetByCaseHeaderIdAsync(int caseHeaderId);
    }
}
