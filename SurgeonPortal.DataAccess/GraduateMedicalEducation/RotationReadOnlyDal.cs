using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.GraduateMedicalEducation
{
    public class RotationReadOnlyDal : SqlServerDalBase, IRotationReadOnlyDal
    {
        public RotationReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<RotationReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<RotationReadOnlyDto>(
                    "[dbo].[get_gmerotations_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
