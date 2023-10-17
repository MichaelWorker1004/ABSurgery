using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface ISanctionsDal
    {
        Task<SanctionsDto> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<SanctionsDto> InsertAsync(SanctionsDto dto);
        Task<SanctionsDto> UpdateAsync(SanctionsDto dto);
    }
}
