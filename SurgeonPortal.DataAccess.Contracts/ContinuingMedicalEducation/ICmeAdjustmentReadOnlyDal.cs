using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation
{
    public interface ICmeAdjustmentReadOnlyDal
    {
        Task<IEnumerable<CmeAdjustmentReadOnlyDto>> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
