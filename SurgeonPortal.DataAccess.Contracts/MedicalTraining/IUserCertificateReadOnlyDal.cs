using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IUserCertificateReadOnlyDal
    {
        Task<IEnumerable<UserCertificateReadOnlyDto>> GetByUserAndTypeAsync(
            int userId,
            int certificateTypeId);
        Task<IEnumerable<UserCertificateReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
