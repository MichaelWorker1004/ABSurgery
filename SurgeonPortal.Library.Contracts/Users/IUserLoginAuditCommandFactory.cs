using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserLoginAuditCommandFactory
    {
        Task<IUserLoginAuditCommand> AuditAsync(int userId, string emailAddress, int applicationId, string loginIpAddress, string loginUserAgent, bool loginSuccess, string loginFailureReason);
    }
}
