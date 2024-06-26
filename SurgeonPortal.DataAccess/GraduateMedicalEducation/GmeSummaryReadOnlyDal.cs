using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.GraduateMedicalEducation
{
    public class GmeSummaryReadOnlyDal : SqlServerDalBase, IGmeSummaryReadOnlyDal
    {
        public GmeSummaryReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<GmeSummaryReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<GmeSummaryReadOnlyDto>(
                    "[dbo].[get_gmesummary_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
