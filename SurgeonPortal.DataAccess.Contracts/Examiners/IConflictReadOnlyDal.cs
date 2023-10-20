using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examiners
{
    public interface IConflictReadOnlyDal
    {
        Task<ConflictReadOnlyDto> GetByExamHeaderIdAsync(
            int examHeaderId,
            int examinerUserId);
    }
}
