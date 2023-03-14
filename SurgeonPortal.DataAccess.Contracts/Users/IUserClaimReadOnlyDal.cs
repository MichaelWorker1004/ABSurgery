using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IUserClaimReadOnlyDal
    {
        Task<IEnumerable<UserClaimReadOnlyDto>> GetByIdsAsync(int userId, int applicationId);
    }
}
