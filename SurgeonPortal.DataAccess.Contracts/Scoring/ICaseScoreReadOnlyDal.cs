using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface ICaseScoreReadOnlyDal
    {
        Task<IEnumerable<CaseScoreReadOnlyDto>> GetByExamScheduleIdAsync(
            int examScheduleId,
            int examinerUserId);
    }
}
