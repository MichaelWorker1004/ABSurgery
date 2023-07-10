using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IDashboardRosterReadOnlyDal
    {
        Task<IEnumerable<DashboardRosterReadOnlyDto>> GetByUserIdAsync(System.DateTime examDate);
    }
}
