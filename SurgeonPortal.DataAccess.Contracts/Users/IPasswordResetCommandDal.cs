using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IPasswordResetCommandDal
    {
        Task<PasswordResetCommandDto> ResetPasswordAsync(
            int userId,
            string oldPassword,
            string newPassword);
    }
}
