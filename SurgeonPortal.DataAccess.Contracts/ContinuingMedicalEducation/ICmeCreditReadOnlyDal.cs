using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation
{
    public interface ICmeCreditReadOnlyDal
    {
        Task<CmeCreditReadOnlyDto> GetByIdAsync(int cmeId);
        Task<IEnumerable<CmeCreditReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
