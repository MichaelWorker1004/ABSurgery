using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ScoringSessionReadOnlyDal : SqlServerDalBase, IScoringSessionReadOnlyDal
    {
        public ScoringSessionReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ScoringSessionReadOnlyDto>> GetByExaminerIdAsync(int examHeaderId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ScoringSessionReadOnlyDto>(
                    "[dbo].[get_day_session_picklist]",
                        new
                        {
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            ExamHeaderId = examHeaderId,
                        });
                        
            }
        }

    }
}
