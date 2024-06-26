using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class UserTokenDal : SqlServerDalBase, IUserTokenDal
    {
        public UserTokenDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<UserTokenDto> GetActiveAsync(string token)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserTokenDto>(
                    "[dbo].[get_usertoken_getactive]",
                        new
                        {
                            Token = token,
                        });
                        
            }
        }

        public async Task<UserTokenDto> InsertAsync(UserTokenDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<UserTokenDto>(
                        "[dbo].[insert_usertoken]",
                            new
                            {
                                UserId = dto.UserId,
                                Token = dto.Token,
                                ExpiresAt = dto.ExpiresAt,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("UserToken");
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
