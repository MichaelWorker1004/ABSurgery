using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class AttestationStatusReadOnlyDal : SqlServerDalBase, IAttestationStatusReadOnlyDal
    {
        public AttestationStatusReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<AttestationStatusReadOnlyDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AttestationStatusReadOnlyDto>(
                    "[dbo].[get_cca_attestation_status_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
