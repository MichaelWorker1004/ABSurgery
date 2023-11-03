using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IPasswordValidationCommandFactory
    {
        IPasswordValidationCommand Validate(
            int userId,
            string password);
    }
}
