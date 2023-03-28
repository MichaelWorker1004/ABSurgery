using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IPasswordValidationCommandDal
    {
        PasswordValidationCommandDto Validate(int userId, string password);
    }
}
