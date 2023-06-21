using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class LicenseTypeReadOnlyDal : SqlServerDalBase, ILicenseTypeReadOnlyDal
    {
        public LicenseTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<LicenseTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<LicenseTypeReadOnlyDto>(
                    "[dbo].[get_license_types]",
                        param: null);
                        
            }
        }

    }
}
