using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring.CE
{
    public class TitleReadOnlyDal : SqlServerDalBase, ITitleReadOnlyDal
    {
        public TitleReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<TitleReadOnlyDto>> GetByIdAsync(
            int examScheduleId,
            int examinerUserId,
            int examineeUserId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<TitleReadOnlyDto>(
                    "[dbo].[get_examinee_case_titles]",
                        new
                        {
                            ExamScheduleId = examScheduleId,
                            ExaminerUserId = examinerUserId,
                            ExamineeUserId = examineeUserId,
                        });
                        
            }
        }

    }
}
