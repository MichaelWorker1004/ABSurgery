using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IResetForgotPasswordCommandFactory
    {
        Task<IResetForgotPasswordCommand> ResetForgotPasswordAsync(
            System.Guid resetGUID,
            string newPassword);
    }
}
