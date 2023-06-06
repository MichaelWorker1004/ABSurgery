using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IOutcomeRegistryDal
    {
        Task<OutcomeRegistryDto> GetByUserIdAsync();
        Task<OutcomeRegistryDto> InsertAsync(OutcomeRegistryDto dto);
        Task<OutcomeRegistryDto> UpdateAsync(OutcomeRegistryDto dto);
    }
}
