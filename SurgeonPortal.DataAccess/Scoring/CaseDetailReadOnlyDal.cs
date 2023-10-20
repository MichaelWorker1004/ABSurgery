using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class CaseDetailReadOnlyDal : SqlServerDalBase, ICaseDetailReadOnlyDal
    {
        public CaseDetailReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<CaseDetailReadOnlyDto>> GetByCaseHeaderIdAsync(
            int caseHeaderId,
            int examinerUserId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CaseDetailReadOnlyDto>(
                    "[dbo].[get_case_details_by_id]",
                        new
                        {
                            CaseHeaderId = caseHeaderId,
                            ExaminerUserId = examinerUserId,
                        });
                        
            }
        }

    }
}
