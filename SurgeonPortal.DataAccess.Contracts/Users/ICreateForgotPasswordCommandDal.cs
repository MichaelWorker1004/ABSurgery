using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface ICreateForgotPasswordCommandDal
    {
        Task<CreateForgotPasswordCommandDto> SendForgotPasswordEmailAsync(string userName);
    }
}
