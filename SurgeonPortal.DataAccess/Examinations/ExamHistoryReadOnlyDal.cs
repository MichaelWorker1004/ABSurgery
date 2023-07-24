using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class ExamHistoryReadOnlyDal : SqlServerDalBase, IExamHistoryReadOnlyDal
    {
        public ExamHistoryReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ExamHistoryReadOnlyDto>> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamHistoryReadOnlyDto>(
                    "[dbo].[get_userexamhistory]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
