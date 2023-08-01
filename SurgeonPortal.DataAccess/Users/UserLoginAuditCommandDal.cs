using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class UserLoginAuditCommandDal : SqlServerDalBase, IUserLoginAuditCommandDal
    {
        public UserLoginAuditCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task AuditAsync(
            int userId,
            string emailAddress,
            int applicationId,
            string loginIpAddress,
            string loginUserAgent,
            bool loginSuccess,
            string loginFailureReason)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[insert_user_login_audit]",
                        new
                        {
                            UserId = userId,
                            EmailAddress = emailAddress,
                            ApplicationId = applicationId,
                            LoginIpAddress = loginIpAddress,
                            LoginUserAgent = loginUserAgent,
                            LoginSuccess = loginSuccess,
                            LoginFailureReason = loginFailureReason,
                            CreatedByUserId = userId,
                            LastUpdatedByUserId = userId,
                        });
                        
            }
        }

    }
}
