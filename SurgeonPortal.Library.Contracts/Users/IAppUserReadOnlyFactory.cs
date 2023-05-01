using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IAppUserReadOnlyFactory
    {
        Task<IAppUserReadOnly> GetByCredentialsAsync(string userName, string password);
        Task<IAppUserReadOnly> GetByTokenAsync(string token);
    }
}
