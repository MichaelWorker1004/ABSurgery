using SurgeonPortal.DataAccess.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class ActiveExamReadOnlyDal : SqlServerDalBase, IActiveExamReadOnlyDal
    {
        public ActiveExamReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ActiveExamReadOnlyDto> GetByExaminerUserIdAsync(
            int examinerUserId,
            DateTime currentDate)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ActiveExamReadOnlyDto>(
                    "[dbo].[get_active_exam_byuserid]",
                        new
                        {
                            ExaminerUserId = examinerUserId,
                            CurrentDate = currentDate,
                        });
                        
            }
        }

    }
}
