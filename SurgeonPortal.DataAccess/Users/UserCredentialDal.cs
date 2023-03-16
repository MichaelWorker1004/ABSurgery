using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class UserCredentialDal : SqlServerDalBase, IUserCredentialDal
    {
        public UserCredentialDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<UserCredentialDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserCredentialDto>(
                    "[dbo].[get_user_account]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

        public async Task<UserCredentialDto> UpdateAsync(UserCredentialDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserCredentialDto>(
                    "[dbo].[upd_user_account]",
                        new
                        {
                            UserId = dto.UserId,
                            EmailAddress = dto.EmailAddress,
                            Password = dto.Password,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

    }
}
