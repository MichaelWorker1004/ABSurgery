using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IUserCertificateDal
    {
        Task DeleteAsync(UserCertificateDto dto);
        Task<UserCertificateDto> GetByIdAsync(int id);
        Task<UserCertificateDto> InsertAsync(UserCertificateDto dto);
    }
}
