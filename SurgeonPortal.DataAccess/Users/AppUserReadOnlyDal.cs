using SurgeonPortal.DataAccess.Contracts.Users;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class AppUserReadOnlyDal : SqlServerDalBase, IAppUserReadOnlyDal
    {
        public AppUserReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }

        public async Task<AppUserReadOnlyDto> GetByCredentialsAsync(
            string emailAddress,
            string password)
        {
            using (var connection = CreateConnection())
            {
                var multi = await connection.QueryMultipleAsync(
                    "[dbo].[get_userlogin]",
                        new
                        {
                            EmailAddress = emailAddress,
                            Password = password,
                        });
                        
                var user = await multi.ReadFirstOrDefaultAsync<AppUserReadOnlyDto>();
                
                if(user == null)
                {
                    return null;
                }

                user.Claims = (await multi.ReadAsync<UserClaimReadOnlyDto>()).ToList();

                return user;
            }
        }

        public async Task<AppUserReadOnlyDto> GetByTokenAsync(string token)
        {
            using (var connection = CreateConnection())
            {
                var multi = await connection.QueryMultipleAsync(
                    "[dbo].[get_user_bytoken]",
                        new
                        {
                            Token = token,
                        });

                var user = await multi.ReadFirstOrDefaultAsync<AppUserReadOnlyDto>();

                if (user == null)
                {
                    return null;
                }

                user.Claims = (await multi.ReadAsync<UserClaimReadOnlyDto>()).ToList();

                return user;
            }
            
        }

    }
}
