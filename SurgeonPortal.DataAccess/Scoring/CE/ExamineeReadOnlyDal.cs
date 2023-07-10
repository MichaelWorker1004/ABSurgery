using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring.CE
{
    public class ExamineeReadOnlyDal : SqlServerDalBase, IExamineeReadOnlyDal
    {
        public ExamineeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ExamineeReadOnlyDto> GetByIdAsync(int examScheduleId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ExamineeReadOnlyDto>(
                    "[dbo].[get_examinee_session_byid]",
                        new
                        {
                            ExamScheduleId = examScheduleId,
                        });
                        
            }
        }

    }
}
