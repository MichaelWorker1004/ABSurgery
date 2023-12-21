using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IVerifyForgotGuidCommandDal
    {
        Task<VerifyForgotGuidCommandDto> VerifyForgotPasswordGuidAsync(System.Guid resetGUID);
    }
}
