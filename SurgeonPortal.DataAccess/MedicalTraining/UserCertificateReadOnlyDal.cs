using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class UserCertificateReadOnlyDal : SqlServerDalBase, IUserCertificateReadOnlyDal
    {
        public UserCertificateReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<UserCertificateReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<UserCertificateReadOnlyDto>(
                    "[dbo].[get_usercertificates_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
