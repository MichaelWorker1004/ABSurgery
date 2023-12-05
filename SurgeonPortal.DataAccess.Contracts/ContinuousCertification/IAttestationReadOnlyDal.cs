using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IAttestationReadOnlyDal
    {
        Task<IEnumerable<AttestationReadOnlyDto>> GetAllByUserIdAsync(int userId);
    }
}
