using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class CaseScoreReadOnlyDal : SqlServerDalBase, ICaseScoreReadOnlyDal
    {
        public CaseScoreReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<CaseScoreReadOnlyDto>> GetByExamScheduleIdAsync(int examScheduleId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CaseScoreReadOnlyDto>(
                    "[dbo].[get_examinerscores_byexamscheduleId]",
                        new
                        {
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            ExamScheduleId = examScheduleId,
                        });
                        
            }
        }

    }
}
