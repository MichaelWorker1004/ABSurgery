using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface IExamStatusReadOnlyDal
    {
        Task<IEnumerable<ExamStatusReadOnlyDto>> GetAllAsync();
    }
}
