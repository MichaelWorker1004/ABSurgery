using SurgeonPortal.DataAccess.Contracts.Surgeons;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Surgeons
{
    public class CertificationReadOnlyDal : SqlServerDalBase, ICertificationReadOnlyDal
    {
        public CertificationReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<CertificationReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CertificationReadOnlyDto>(
                    "[dbo].[get_user_certifications]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
