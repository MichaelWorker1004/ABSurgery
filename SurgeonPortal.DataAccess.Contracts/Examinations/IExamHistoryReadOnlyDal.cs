using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IExamHistoryReadOnlyDal
    {
        Task<IEnumerable<ExamHistoryReadOnlyDto>> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
