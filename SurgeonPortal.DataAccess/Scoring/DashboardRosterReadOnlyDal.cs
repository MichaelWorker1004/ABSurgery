using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class DashboardRosterReadOnlyDal : SqlServerDalBase, IDashboardRosterReadOnlyDal
    {
        public DashboardRosterReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<DashboardRosterReadOnlyDto>> GetByUserIdAsync(System.DateTime examDate)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<DashboardRosterReadOnlyDto>(
                    "[dbo].[get_examinerschedule_byuserid]",
                        new
                        {
                            ExaminerUserId = examinerUserId,
                            ExamDate = examDate,
                        });
                        
            }
        }

    }
}
