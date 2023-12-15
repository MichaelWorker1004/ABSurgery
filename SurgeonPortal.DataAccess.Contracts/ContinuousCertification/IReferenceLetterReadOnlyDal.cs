using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IReferenceLetterReadOnlyDal
    {
        Task<IEnumerable<ReferenceLetterReadOnlyDto>> GetAllByUserIdAsync(int userId);
    }
}
