using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IPasswordResetCommandDal
    {
        Task<PasswordResetCommandDto> ResetPasswordAsync(
            string oldPassword,
            string newPassword,
            int userId);
    }
}
