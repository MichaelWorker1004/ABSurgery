using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserTokenFactory
    {
        Task<IUserToken> GetActiveAsync(string token);
        IUserToken Create();
    }
}
