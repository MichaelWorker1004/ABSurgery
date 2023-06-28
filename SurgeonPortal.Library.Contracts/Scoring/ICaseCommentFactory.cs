using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseCommentFactory
    {
        Task<ICaseComment> GetByIdAsync(int id);
        ICaseComment Create();
    }
}
