using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IMedicalLicenseReadOnlyListFactory
    {
        Task<IMedicalLicenseReadOnlyList> GetByUserIdAsync();
    }
}
