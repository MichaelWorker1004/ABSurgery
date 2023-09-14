using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class PasswordResetCommandDal : SqlServerDalBase, IPasswordResetCommandDal
    {
        public PasswordResetCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<PasswordResetCommandDto> ResetPasswordAsync(
            string oldPassword,
            string newPassword)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<PasswordResetCommandDto>(
                    "[dbo].[update_user_password]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            OldPassword = oldPassword,
                            NewPassword = newPassword,
                        });
                        
            }
        }

    }
}
