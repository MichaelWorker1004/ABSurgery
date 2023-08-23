using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class IsExamSessionLockedCommandDal : SqlServerDalBase, IIsExamSessionLockedCommandDal
    {
        public IsExamSessionLockedCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public IsExamSessionLockedCommandDto IsExamSessionLocked(int examCaseId)
        {
            using (var connection = CreateConnection())
            {
                return connection.ExecFirstOrDefault<IsExamSessionLockedCommandDto>(
                    "[dbo].[get_isLocked_by_examId]",
                        new
                        {
                            ExamCaseId = examCaseId,
                        });
                        
            }
        }

    }
}
