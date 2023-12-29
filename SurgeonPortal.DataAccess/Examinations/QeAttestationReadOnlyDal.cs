using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class QeAttestationReadOnlyDal : SqlServerDalBase, IQeAttestationReadOnlyDal
    {
        public QeAttestationReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<QeAttestationReadOnlyDto>> GetByExamIdAsync(
            int userId,
            int examId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<QeAttestationReadOnlyDto>(
                    "[dbo].[get_qe_attestation_items_by_userId]",
                        new
                        {
                            UserId = userId,
                            ExamId = examId,
                        });
                        
            }
        }

    }
}
