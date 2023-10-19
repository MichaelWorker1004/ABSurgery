using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class ExamSessionSkipCommandDal : SqlServerDalBase, IExamSessionSkipCommandDal
    {
        public ExamSessionSkipCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task SkipExamSessionAsync(
            int examScheduleId,
            _identity.GetUserId<int>())
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[update_skip_exam]",
                        new
                        {
                            ExamScheduleId = examScheduleId,
                            ExaminerUserId = examinerUserId,
                        });
                        
            }
        }

    }
}
