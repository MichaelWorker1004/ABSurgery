using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class ExamSessionReadOnlyDal : SqlServerDalBase, IExamSessionReadOnlyDal
    {
        public ExamSessionReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ExamSessionReadOnlyDto>> GetByUserIdAsync(System.DateTime examDate)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamSessionReadOnlyDto>(
                    "[dbo].[get_examinee_sessions]",
                        new
                        {
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            ExamDate = examDate,
                        });
                        
            }
        }

    }
}
