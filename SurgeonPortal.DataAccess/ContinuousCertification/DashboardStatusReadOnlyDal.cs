using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class DashboardStatusReadOnlyDal : SqlServerDalBase, IDashboardStatusReadOnlyDal
    {
        public DashboardStatusReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<DashboardStatusReadOnlyDto>> GetAllByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<DashboardStatusReadOnlyDto>(
                    "[dbo].[get_all_status]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
