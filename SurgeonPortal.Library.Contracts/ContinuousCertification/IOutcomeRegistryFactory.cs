using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IOutcomeRegistryFactory
    {
        Task<IOutcomeRegistry> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
        IOutcomeRegistry Create();
    }
}
