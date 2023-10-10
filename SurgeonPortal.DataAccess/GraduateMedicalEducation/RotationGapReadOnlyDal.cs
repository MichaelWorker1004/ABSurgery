using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.GraduateMedicalEducation
{
    public class RotationGapReadOnlyDal : SqlServerDalBase, IRotationGapReadOnlyDal
    {
        public RotationGapReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<RotationGapReadOnlyDto>> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<RotationGapReadOnlyDto>(
                    "[dbo].[get_gme_conflicts]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
