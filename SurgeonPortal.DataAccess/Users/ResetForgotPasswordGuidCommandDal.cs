using SurgeonPortal.DataAccess.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class ResetForgotPasswordGuidCommandDal : SqlServerDalBase, IResetForgotPasswordGuidCommandDal
    {
        public ResetForgotPasswordGuidCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }


        public async Task<Guid> GetResetForgotPasswordGUIDAsync(
            int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<Guid>(
                    "[dbo].[get_reset_passwork_guid]",
                        new
                        {
                            UserId = userId
                        });

            }
        }

    }
}
