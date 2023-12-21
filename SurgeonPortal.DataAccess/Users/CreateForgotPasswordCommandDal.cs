using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class CreateForgotPasswordCommandDal : SqlServerDalBase, ICreateForgotPasswordCommandDal
    {
        public CreateForgotPasswordCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<CreateForgotPasswordCommandDto> SendForgotPasswordEmailAsync(string userName)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CreateForgotPasswordCommandDto>(
                    "[dbo].[ins_reset_guid]",
                        new
                        {
                            UserName = userName,
                        });
                        
            }
        }

    }
}
