using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IPasswordValidationCommandDal
    {
        Task<PasswordValidationCommandDto> ValidateAsync(int userId, string password);
    }
}
