using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Billing
{
    public interface IExamFeeReadOnlyDal
    {
        Task<IEnumerable<ExamFeeReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
