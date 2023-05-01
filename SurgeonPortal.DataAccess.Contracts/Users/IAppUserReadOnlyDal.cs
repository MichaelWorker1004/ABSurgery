using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IAppUserReadOnlyDal
    {
        Task<AppUserReadOnlyDto> GetByCredentialsAsync(string userName, string password);
        Task<AppUserReadOnlyDto> GetByTokenAsync(string token);
    }
}
