using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class ExamSessionLockCommandDal : SqlServerDalBase, IExamSessionLockCommandDal
    {
        public ExamSessionLockCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task LockExamSessionAsync(int examscheduleId)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[lock_exam_scoring]",
                        new
                        {
                            ExamscheduleId = examscheduleId,
                        });
                        
            }
        }

    }
}
