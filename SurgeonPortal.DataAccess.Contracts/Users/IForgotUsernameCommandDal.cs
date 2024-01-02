using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IForgotUsernameCommandDal
    {
        Task<ForgotUsernameCommandDto> SendForgotUsernameEmailAsync(string email);
    }
}
