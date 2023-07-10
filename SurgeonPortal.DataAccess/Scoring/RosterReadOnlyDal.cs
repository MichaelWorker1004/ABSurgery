using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class RosterReadOnlyDal : SqlServerDalBase, IRosterReadOnlyDal
    {
        public RosterReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<RosterReadOnlyDto>> GetByExaminationHeaderIdAsync(int examHeaderId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<RosterReadOnlyDto>(
                    "[dbo].[get_examination_scores]",
                        new
                        {
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            ExamHeaderId = examHeaderId,
                        });
                        
            }
        }

    }
}
