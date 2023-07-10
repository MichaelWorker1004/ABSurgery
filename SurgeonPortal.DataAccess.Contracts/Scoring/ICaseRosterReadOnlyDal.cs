using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface ICaseRosterReadOnlyDal
    {
        Task<IEnumerable<CaseRosterReadOnlyDto>> GetByScheduleIdAsync(int scheduleId1, int? scheduleId2);
    }
}
