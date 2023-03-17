using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IPasswordValidationCommandFactory
    {
        Task<IPasswordValidationCommand> ValidateAsync(int userId, string password);
    }
}
