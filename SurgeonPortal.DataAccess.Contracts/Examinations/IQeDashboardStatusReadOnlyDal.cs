using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IQeDashboardStatusReadOnlyDal
    {
        Task<IEnumerable<QeDashboardStatusReadOnlyDto>> GetByExamIdAsync(
            int userId,
            int examheaderId);
    }
}
