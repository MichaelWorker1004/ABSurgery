using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IRequirementsReadOnlyDal
    {
        Task<RequirementsReadOnlyDto> GetByUserIdAsync(int userId);
    }
}
