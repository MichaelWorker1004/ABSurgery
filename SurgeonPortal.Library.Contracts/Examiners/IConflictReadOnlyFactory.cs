using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IConflictReadOnlyFactory
    {
        Task<IConflictReadOnly> GetByExamHeaderIdAsync(int examHeaderId);
    }
}
