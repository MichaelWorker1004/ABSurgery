using SurgeonPortal.DataAccess.Contracts.Billing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Billing
{
    public class ExamFeeReadOnlyDal : SqlServerDalBase, IExamFeeReadOnlyDal
    {
        public ExamFeeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ExamFeeReadOnlyDto> GetByExamIdAsync(
            int userId,
            int examId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ExamFeeReadOnlyDto>(
                    "[dbo].[get_exam_fee_by_examId]",
                        new
                        {
                            UserId = userId,
                            ExamId = examId,
                        });
                        
            }
        }

        public async Task<IEnumerable<ExamFeeReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamFeeReadOnlyDto>(
                    "[dbo].[Get_Exam_Fees_byUserId]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
