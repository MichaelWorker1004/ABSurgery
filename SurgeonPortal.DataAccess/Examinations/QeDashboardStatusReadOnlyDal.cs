using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class QeDashboardStatusReadOnlyDal : SqlServerDalBase, IQeDashboardStatusReadOnlyDal
    {
        public QeDashboardStatusReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<QeDashboardStatusReadOnlyDto>> GetByExamIdAsync(
            int userId,
            int examheaderId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<QeDashboardStatusReadOnlyDto>(
                    "[dbo].[get_user_qe_all_status_byuserid]",
                        new
                        {
                            UserId = userId,
                            ExamheaderId = examheaderId,
                        });
                        
            }
        }

    }
}
