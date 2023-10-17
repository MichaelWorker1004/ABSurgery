using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IOtherCertificationsDal
    {
        Task DeleteAsync(OtherCertificationsDto dto);
        Task<OtherCertificationsDto> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<OtherCertificationsDto> InsertAsync(OtherCertificationsDto dto);
        Task<OtherCertificationsDto> UpdateAsync(OtherCertificationsDto dto);
    }
}
