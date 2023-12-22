using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Billing
{
    public interface IExamFeeReadOnlyFactory
    {
        Task<IExamFeeReadOnly> GetByExamIdAsync(int examId);
    }
}
