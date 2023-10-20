using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public interface IGmeSummaryReadOnlyDal
    {
        Task<IEnumerable<GmeSummaryReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
