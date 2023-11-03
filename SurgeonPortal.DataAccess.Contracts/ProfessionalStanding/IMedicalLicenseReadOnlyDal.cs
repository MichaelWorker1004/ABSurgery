using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IMedicalLicenseReadOnlyDal
    {
        Task<IEnumerable<MedicalLicenseReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
