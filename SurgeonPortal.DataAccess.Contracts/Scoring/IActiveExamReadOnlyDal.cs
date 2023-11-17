using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IActiveExamReadOnlyDal
    {
        Task<ActiveExamReadOnlyDto> GetByExaminerUserIdAsync(
            int examinerUserId,
            System.DateTime currentDate);
    }
}
