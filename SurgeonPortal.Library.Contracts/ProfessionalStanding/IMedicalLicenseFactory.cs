using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IMedicalLicenseFactory
    {
        Task<IMedicalLicense> GetByIdAsync(int licenseId);
        IMedicalLicense Create();
    }
}
