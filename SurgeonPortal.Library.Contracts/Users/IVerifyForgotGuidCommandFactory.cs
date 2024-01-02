using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IVerifyForgotGuidCommandFactory
    {
        Task<IVerifyForgotGuidCommand> VerifyForgotPasswordGuidAsync(System.Guid resetGUID);
    }
}
