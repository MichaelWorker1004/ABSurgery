using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IAttestationReadOnlyListFactory
    {
        Task<IAttestationReadOnlyList> GetAllByUserIdAsync();
    }
}
