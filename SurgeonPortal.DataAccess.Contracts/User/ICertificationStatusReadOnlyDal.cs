using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.User
{
    public interface ICertificationStatusReadOnlyDal
    {
        Task<CertificationStatusReadOnlyDto> GetByUserIdAsync(int userId);
    }
}
