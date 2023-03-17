using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class PasswordValidationCommandDal : SqlServerDalBase, IPasswordValidationCommandDal
    {
        public PasswordValidationCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<PasswordValidationCommandDto> ValidateAsync(
            int userId,
            string password)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<PasswordValidationCommandDto>(
                    "[dbo].[get_user_passwordvalidate]",
                        new
                        {
                            UserId = userId,
                            Password = password,
                        });
                        
            }
        }

    }
}
