using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class AttestationReadOnlyDal : SqlServerDalBase, IAttestationReadOnlyDal
    {
        public AttestationReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<AttestationReadOnlyDto>> GetAllByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<AttestationReadOnlyDto>(
                    "[dbo].[get_cca_attestation_items_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
