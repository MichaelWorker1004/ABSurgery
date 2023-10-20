using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface ISanctionsDal
    {
        Task<SanctionsDto> GetByUserIdAsync(int userId);
        Task<SanctionsDto> InsertAsync(SanctionsDto dto);
        Task<SanctionsDto> UpdateAsync(SanctionsDto dto);
    }
}
