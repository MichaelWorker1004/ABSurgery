using System;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IResetForgotPasswordCommandDal
    {
        Task<ResetForgotPasswordCommandDto> ResetForgotPasswordAsync(
            System.Guid resetGUID,
            string newPassword);

    }
}
