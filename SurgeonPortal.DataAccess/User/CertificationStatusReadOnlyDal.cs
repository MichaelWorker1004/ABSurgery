using SurgeonPortal.DataAccess.Contracts.User;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.User
{
    public class CertificationStatusReadOnlyDal : SqlServerDalBase, ICertificationStatusReadOnlyDal
    {
        public CertificationStatusReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<CertificationStatusReadOnlyDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CertificationStatusReadOnlyDto>(
                    "[dbo].[get_user_certification_current_status_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
