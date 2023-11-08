using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IPasswordResetCommandFactory
    {
        Task<IPasswordResetCommand> ResetPasswordAsync(
            string oldPassword,
            string newPassword);
    }
}
