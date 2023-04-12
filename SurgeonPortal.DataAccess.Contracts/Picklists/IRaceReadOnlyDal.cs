using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IRaceReadOnlyDal
    {
        Task<IEnumerable<RaceReadOnlyDto>> GetAllAsync();
    }
}
