using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Billing
{
    public interface IApplicationFeeReadOnlyFactory
    {
        Task<IApplicationFeeReadOnly> GetByExamIdAsync(
            int userId,
            int examId);
    }
}
