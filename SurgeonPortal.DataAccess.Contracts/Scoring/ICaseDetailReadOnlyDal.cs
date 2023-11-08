using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface ICaseDetailReadOnlyDal
    {
        Task<IEnumerable<CaseDetailReadOnlyDto>> GetByCaseHeaderIdAsync(
            int caseHeaderId,
            int examinerUserId);
    }
}
