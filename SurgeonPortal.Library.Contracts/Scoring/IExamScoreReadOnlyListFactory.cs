using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamScoreReadOnlyListFactory
    {
        Task<IExamScoreReadOnlyList> GetByIdAsync();
    }
}
