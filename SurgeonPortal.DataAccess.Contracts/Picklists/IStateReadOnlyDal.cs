using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IStateReadOnlyDal
    {
        Task<IEnumerable<StateReadOnlyDto>> GetByCountryAsync(string countryCode);
    }
}
