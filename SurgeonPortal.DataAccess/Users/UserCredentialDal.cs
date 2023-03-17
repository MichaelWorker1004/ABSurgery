using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Shared;
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



        public async Task<UserCredentialDto> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserCredentialDto>(
                    "[dbo].[get_user_account]",
                        new
                        {
                            UserId = IdentityHelper.UserId,
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
                            UserId = IdentityHelper.UserId,
                            EmailAddress = dto.EmailAddress,
                            Password = dto.Password,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

    }
}
