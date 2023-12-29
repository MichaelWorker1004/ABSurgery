using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IQeExamEligibilityReadOnlyDal
    {
        Task<IEnumerable<QeExamEligibilityReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
