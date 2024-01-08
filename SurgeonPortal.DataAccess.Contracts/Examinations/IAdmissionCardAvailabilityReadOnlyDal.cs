using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IAdmissionCardAvailabilityReadOnlyDal
    {
        Task<AdmissionCardAvailabilityReadOnlyDto> GetByExamIdAsync(
            int userID,
            int examID);
    }
}
