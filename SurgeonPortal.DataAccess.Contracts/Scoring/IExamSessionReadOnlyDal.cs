using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IExamSessionReadOnlyDal
    {
        Task<IEnumerable<ExamSessionReadOnlyDto>> GetByUserIdAsync(System.DateTime examDate);
    }
}
