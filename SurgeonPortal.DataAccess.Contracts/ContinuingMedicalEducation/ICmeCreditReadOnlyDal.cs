using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation
{
    public interface ICmeCreditReadOnlyDal
    {
        Task<CmeCreditReadOnlyDto> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<IEnumerable<CmeCreditReadOnlyDto>> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
