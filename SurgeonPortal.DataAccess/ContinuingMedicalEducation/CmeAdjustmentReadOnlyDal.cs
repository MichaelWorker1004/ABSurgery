using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuingMedicalEducation
{
    public class CmeAdjustmentReadOnlyDal : SqlServerDalBase, ICmeAdjustmentReadOnlyDal
    {
        public CmeAdjustmentReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<CmeAdjustmentReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CmeAdjustmentReadOnlyDto>(
                    "[dbo].[get_usercme_waivers_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
