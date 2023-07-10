using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring.CE
{
    public interface ITitleReadOnlyDal
    {
        Task<IEnumerable<TitleReadOnlyDto>> GetByIdAsync(int examScheduleId);
    }
}
