using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IExamScoreReadOnlyDal
    {
        Task<IEnumerable<ExamScoreReadOnlyDto>> GetByIdAsync();
    }
}
