using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Billing
{
    public interface IApplicationFeeReadOnlyDal
    {
        Task<ApplicationFeeReadOnlyDto> GetByExamIdAsync(
            int userId,
            int examId);
    }
}
