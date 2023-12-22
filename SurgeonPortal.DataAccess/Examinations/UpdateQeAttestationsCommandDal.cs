using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class UpdateQeAttestationsCommandDal : SqlServerDalBase, IUpdateQeAttestationsCommandDal
    {
        public UpdateQeAttestationsCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task UpdateQeAttestationsAsync(
            int userId,
            int examId)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[update_qe_attestation_by_userid]",
                        new
                        {
                            UserId = userId,
                            ExamId = examId,
                        });
                        
            }
        }

    }
}
