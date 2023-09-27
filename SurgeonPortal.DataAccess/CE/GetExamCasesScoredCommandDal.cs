using SurgeonPortal.DataAccess.Contracts.CE;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.CE
{
    public class GetExamCasesScoredCommandDal : SqlServerDalBase, IGetExamCasesScoredCommandDal
    {
        public GetExamCasesScoredCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public GetExamCasesScoredCommandDto GetExamCasesScored(int examScheduleId)
        {
            using (var connection = CreateConnection())
            {
                return connection.ExecFirstOrDefault<GetExamCasesScoredCommandDto>(
                    "[dbo].[get_cases_scored]",
                        new
                        {
                            ExamScheduleId = examScheduleId,
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
