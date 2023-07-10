using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class CaseRosterReadOnlyDal : SqlServerDalBase, ICaseRosterReadOnlyDal
    {
        public CaseRosterReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<CaseRosterReadOnlyDto>> GetByScheduleIdAsync(
            int scheduleId1,
            int? scheduleId2)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CaseRosterReadOnlyDto>(
                    "[dbo].[get_toc_case_list]",
                        new
                        {
                            ScheduleId1 = scheduleId1,
                            ScheduleId2 = scheduleId2,
                        });
                        
            }
        }

    }
}
