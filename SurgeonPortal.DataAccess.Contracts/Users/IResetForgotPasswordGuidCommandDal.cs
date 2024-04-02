using System;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IResetForgotPasswordGuidCommandDal
    {
        Task<Guid> GetResetForgotPasswordGUIDAsync(
            int userId);

    }
}
