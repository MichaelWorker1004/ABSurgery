using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IUserCertificateDal
    {
        Task DeleteAsync(UserCertificateDto dto);
        Task<UserCertificateDto> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<UserCertificateDto> InsertAsync(UserCertificateDto dto);
    }
}
