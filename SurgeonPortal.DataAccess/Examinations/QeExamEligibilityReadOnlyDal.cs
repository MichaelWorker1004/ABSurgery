using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class QeExamEligibilityReadOnlyDal : SqlServerDalBase, IQeExamEligibilityReadOnlyDal
    {
        public QeExamEligibilityReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<QeExamEligibilityReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<QeExamEligibilityReadOnlyDto>(
                    "[dbo].[get_qe_eligibility_by_userId]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
