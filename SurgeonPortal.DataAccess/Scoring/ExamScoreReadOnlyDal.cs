using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class ExamScoreReadOnlyDal : SqlServerDalBase, IExamScoreReadOnlyDal
    {
        public ExamScoreReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ExamScoreReadOnlyDto>> GetByIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamScoreReadOnlyDto>(
                    "[dbo].[get_examination_scores]",
                        new
                        {
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
