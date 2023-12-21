using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class ForgotUsernameCommandDal : SqlServerDalBase, IForgotUsernameCommandDal
    {
        public ForgotUsernameCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ForgotUsernameCommandDto> SendForgotUsernameEmailAsync(string email)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ForgotUsernameCommandDto>(
                    "[dbo].[get_forgotten_username]",
                        new
                        {
                            Email = email,
                        });
                        
            }
        }

    }
}
