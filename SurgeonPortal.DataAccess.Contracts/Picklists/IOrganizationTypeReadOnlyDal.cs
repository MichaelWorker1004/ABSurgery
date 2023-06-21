using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IOrganizationTypeReadOnlyDal
    {
        Task<IEnumerable<OrganizationTypeReadOnlyDto>> GetAllAsync();
    }
}
