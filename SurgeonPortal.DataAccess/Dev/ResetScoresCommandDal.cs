using SurgeonPortal.DataAccess.Contracts.Dev;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Dev
{
    public class ResetScoresCommandDal : SqlServerDalBase, IResetScoresCommandDal
    {
        public ResetScoresCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task ResetExamScoresAsync()
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[dev_reset_scoring_by_ExaminerId]",
                        new
                        {
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
