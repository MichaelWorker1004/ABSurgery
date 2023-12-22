using SurgeonPortal.DataAccess.Contracts.Billing;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Billing
{
    public class ApplicationFeeReadOnlyDal : SqlServerDalBase, IApplicationFeeReadOnlyDal
    {
        public ApplicationFeeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ApplicationFeeReadOnlyDto> GetByExamIdAsync(
            int userId,
            int examId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ApplicationFeeReadOnlyDto>(
                    "[dbo].[get_application_fee_by_examId]",
                        new
                        {
                            UserId = userId,
                            ExamId = examId,
                        });
                        
            }
        }

    }
}
