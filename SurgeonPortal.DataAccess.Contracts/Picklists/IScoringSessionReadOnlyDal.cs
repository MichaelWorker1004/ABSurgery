using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IScoringSessionReadOnlyDal
    {
        Task<IEnumerable<ScoringSessionReadOnlyDto>> GetByKeysAsync(int examHeaderId);
    }
}
