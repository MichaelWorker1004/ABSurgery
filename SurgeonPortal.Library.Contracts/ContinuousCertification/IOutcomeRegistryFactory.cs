using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IOutcomeRegistryFactory
    {
        Task<IOutcomeRegistry> GetByUserIdAsync();
        IOutcomeRegistry Create();
    }
}
