using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IDashboardStatusReadOnlyDal
    {
        Task<IEnumerable<DashboardStatusReadOnlyDto>> GetAllByUserIdAsync(int userId);
    }
}
