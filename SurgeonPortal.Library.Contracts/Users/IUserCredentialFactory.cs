using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserCredentialFactory
    {
        Task<IUserCredential> GetByUserIdAsync();
    }
}
