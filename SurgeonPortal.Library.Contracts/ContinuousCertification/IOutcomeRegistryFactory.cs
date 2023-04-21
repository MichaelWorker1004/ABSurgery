using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IOutcomeRegistryFactory
    {
        Task<IOutcomeRegistry> GetByUserIdAsync(int userId);
        IOutcomeRegistry Create();
    }
}
