using SurgeonPortal.DataAccess.Contracts.Dev;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Dev
{
    public class ResetCaseCommentsCommandDal : SqlServerDalBase, IResetCaseCommentsCommandDal
    {
        public ResetCaseCommentsCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task ResetCaseCommentsAsync(int examinerUserId)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[dev_reset_case_comments_by_ExaminerId]",
                        new
                        {
                            ExaminerUserId = examinerUserId,
                        });
                        
            }
        }

    }
}
