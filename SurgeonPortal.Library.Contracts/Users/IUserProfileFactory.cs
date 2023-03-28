using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserProfileFactory
    {
        Task<IUserProfile> GetByUserIdAsync(int userId);
        IUserProfile Create();
    }
}
