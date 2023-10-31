using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class CaseFeedbackReadOnlyDal : SqlServerDalBase, ICaseFeedbackReadOnlyDal
    {
        public CaseFeedbackReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<CaseFeedbackReadOnlyDto> GetByExaminerIdAsync(
            int examinerUserId,
            int caseHeaderId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseFeedbackReadOnlyDto>(
                    "[dbo].[get_case_feedback_by_examinerId]",
                        new
                        {
                            ExaminerUserId = examinerUserId,
                            CaseHeaderId = caseHeaderId,
                        });
                        
            }
        }

    }
}
