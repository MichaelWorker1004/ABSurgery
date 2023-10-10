using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IOutcomeRegistryDal
    {
        Task<OutcomeRegistryDto> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<OutcomeRegistryDto> InsertAsync(OutcomeRegistryDto dto);
        Task<OutcomeRegistryDto> UpdateAsync(OutcomeRegistryDto dto);
    }
}
