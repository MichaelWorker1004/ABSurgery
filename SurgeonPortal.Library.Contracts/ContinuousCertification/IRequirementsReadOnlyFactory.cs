using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IRequirementsReadOnlyFactory
    {
        Task<IRequirementsReadOnly> GetByUserIdAsync(int userId);
    }
}
