using SurgeonPortal.DataAccess.Contracts.Examiners;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examiners
{
    public class ConflictReadOnlyDal : SqlServerDalBase, IConflictReadOnlyDal
    {
        public ConflictReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ConflictReadOnlyDto> GetByExamHeaderIdAsync(
            int examHeaderId,
            int examinerUserId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ConflictReadOnlyDto>(
                    "[dbo].[get_examiner_conflicts]",
                        new
                        {
                            ExaminerUserId = examinerUserId,
                            ExamHeaderId = examHeaderId,
                        });
                        
            }
        }

    }
}
