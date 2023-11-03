using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IUserLoginAuditCommandDal
    {
        Task AuditAsync(
            int userId,
            string emailAddress,
            int applicationId,
            string loginIpAddress,
            string loginUserAgent,
            bool loginSuccess,
            string loginFailureReason);
    }
}
