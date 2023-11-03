using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ProfessionalStanding
{
    public class MedicalLicenseReadOnlyDal : SqlServerDalBase, IMedicalLicenseReadOnlyDal
    {
        public MedicalLicenseReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<MedicalLicenseReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<MedicalLicenseReadOnlyDto>(
                    "[dbo].[get_userlicenses_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
