using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IOtherCertificationsDal
    {
        Task DeleteAsync(OtherCertificationsDto dto);
        Task<OtherCertificationsDto> GetByIdAsync(int id);
        Task<OtherCertificationsDto> InsertAsync(OtherCertificationsDto dto);
        Task<OtherCertificationsDto> UpdateAsync(OtherCertificationsDto dto);
    }
}
