using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IRosterReadOnlyDal
    {
        Task<IEnumerable<RosterReadOnlyDto>> GetByExaminationHeaderIdAsync(
            int examinerUserId,
            int examHeaderId);
    }
}
