using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IActiveExamReadOnlyFactory
    {
        Task<IActiveExamReadOnly> GetByExaminerUserIdAsync(System.DateTime currentDate);
    }
}
