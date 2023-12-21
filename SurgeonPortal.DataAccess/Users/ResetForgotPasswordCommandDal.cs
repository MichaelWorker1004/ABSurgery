using SurgeonPortal.DataAccess.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class ResetForgotPasswordCommandDal : SqlServerDalBase, IResetForgotPasswordCommandDal
    {
        public ResetForgotPasswordCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ResetForgotPasswordCommandDto> ResetForgotPasswordAsync(
            Guid resetGUID,
            string newPassword)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ResetForgotPasswordCommandDto>(
                    "[dbo].[reset_passwork_using_guid]",
                        new
                        {
                            ResetGUID = resetGUID,
                            NewPassword = newPassword,
                        });
                        
            }
        }

    }
}
