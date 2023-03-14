using SurgeonPortal.DataAccess.Contracts.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class UserClaimReadOnlyDal : SqlServerDalBase, IUserClaimReadOnlyDal
    {
        public UserClaimReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<UserClaimReadOnlyDto>> GetByIdsAsync(
            int userId,
            int applicationId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<UserClaimReadOnlyDto>(
                    "[dbo].[get_user_claims]",
                        new
                        {
                            UserId = userId,
                            ApplicationId = applicationId,
                        });
                        
            }
        }

    }
}
