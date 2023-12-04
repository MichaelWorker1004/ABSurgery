using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IOutcomeRegistryDal
    {
        Task<OutcomeRegistryDto> GetByUserIdAsync(int userID);
        Task<OutcomeRegistryDto> InsertAsync(OutcomeRegistryDto dto);
        Task<OutcomeRegistryDto> UpdateAsync(OutcomeRegistryDto dto);
    }
}
