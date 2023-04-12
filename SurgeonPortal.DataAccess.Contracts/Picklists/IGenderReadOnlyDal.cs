using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IGenderReadOnlyDal
    {
        Task<IEnumerable<GenderReadOnlyDto>> GetAllAsync();
    }
}
