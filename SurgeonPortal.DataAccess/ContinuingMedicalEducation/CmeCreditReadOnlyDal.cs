using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuingMedicalEducation
{
    public class CmeCreditReadOnlyDal : SqlServerDalBase, ICmeCreditReadOnlyDal
    {
        public CmeCreditReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<CmeCreditReadOnlyDto> GetByIdAsync(int cmeId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CmeCreditReadOnlyDto>(
                    "[dbo].[get_usercme_byid]",
                        new
                        {
                            CmeId = cmeId,
                        });
                        
            }
        }

        public async Task<IEnumerable<CmeCreditReadOnlyDto>> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CmeCreditReadOnlyDto>(
                    "[dbo].[get_usercme_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
