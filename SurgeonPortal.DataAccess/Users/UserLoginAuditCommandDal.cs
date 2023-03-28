using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Shared;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.Identity;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class UserLoginAuditCommandDal : SqlServerDalBase, IUserLoginAuditCommandDal
    {
        public UserLoginAuditCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<UserLoginAuditCommandDto> AuditAsync(
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
                return await connection.ExecFirstOrDefaultAsync<UserLoginAuditCommandDto>(
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
