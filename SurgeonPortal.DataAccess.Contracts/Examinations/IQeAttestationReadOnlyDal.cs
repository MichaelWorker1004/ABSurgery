using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IQeAttestationReadOnlyDal
    {
        Task<IEnumerable<QeAttestationReadOnlyDto>> GetByExamIdAsync(
            int userId,
            int examId);
    }
}
