using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IExamHistoryReadOnlyDal
    {
        Task<IEnumerable<ExamHistoryReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
