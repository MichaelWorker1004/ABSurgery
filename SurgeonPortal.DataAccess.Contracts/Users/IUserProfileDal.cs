using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IUserProfileDal
    {
        Task<UserProfileDto> GetByUserIdAsync(int userId);
        Task<UserProfileDto> InsertAsync(UserProfileDto dto);
        Task<UserProfileDto> UpdateAsync(UserProfileDto dto);
    }
}
