using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IAttestationStatusReadOnlyFactory
    {
        Task<IAttestationStatusReadOnly> GetByUserIdAsync();
    }
}
