using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IExamOverviewReadOnlyDal
    {
        Task<IEnumerable<ExamOverviewReadOnlyDto>> GetAllAsync(System.Collections.Generic.List`1[System.String]);
    }
}
