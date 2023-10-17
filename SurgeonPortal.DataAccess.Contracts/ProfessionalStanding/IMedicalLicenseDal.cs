using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IMedicalLicenseDal
    {
        Task DeleteAsync(MedicalLicenseDto dto);
        Task<MedicalLicenseDto> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<MedicalLicenseDto> InsertAsync(MedicalLicenseDto dto);
        Task<MedicalLicenseDto> UpdateAsync(MedicalLicenseDto dto);
    }
}
