using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface ICreateForgotPasswordCommandFactory
    {
        Task<ICreateForgotPasswordCommand> SendForgotPasswordEmailAsync(string userName);
    }
}
