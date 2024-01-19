using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class ExamOverviewReadOnlyDal : SqlServerDalBase, IExamOverviewReadOnlyDal
    {
        public ExamOverviewReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ExamOverviewReadOnlyDto>> GetAllAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamOverviewReadOnlyDto>(
                    "[dbo].[get_exam_overview]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
