using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IForgotUsernameCommandFactory
    {
        Task<IForgotUsernameCommand> SendForgotUsernameEmailAsync(string email);
    }
}
