using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class InsertUserExamCommandDal : SqlServerDalBase, IInsertUserExamCommandDal
    {
        public InsertUserExamCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task InsertUserExamAsync(
            int userId,
            int examHeaderId)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[ins_user_exam]",
                        new
                        {
                            UserId = userId,
                            ExamHeaderId = examHeaderId,
                        });
                        
            }
        }

    }
}
