using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IAccommodationReadOnlyDal
    {
        Task<IEnumerable<AccommodationReadOnlyDto>> GetAllAsync();
    }
}
