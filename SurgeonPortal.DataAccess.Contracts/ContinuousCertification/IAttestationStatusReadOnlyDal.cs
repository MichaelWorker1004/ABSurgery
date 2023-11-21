using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IAttestationStatusReadOnlyDal
    {
        Task<AttestationStatusReadOnlyDto> GetByUserIdAsync(int userId);
    }
}
