using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class OtherCertificationsReadOnlyDal : SqlServerDalBase, IOtherCertificationsReadOnlyDal
    {
        public OtherCertificationsReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<OtherCertificationsReadOnlyDto>> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<OtherCertificationsReadOnlyDto>(
                    "[dbo].[get_user_certificates_other_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
