using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Billing
{
    public interface IExamFeeReadOnlyDal
    {
        Task<ExamFeeReadOnlyDto> GetByExamIdAsync(
            int userId,
            int examId);
        Task<IEnumerable<ExamFeeReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
