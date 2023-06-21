using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class JcahoOrganizationReadOnlyDal : SqlServerDalBase, IJcahoOrganizationReadOnlyDal
    {
        public JcahoOrganizationReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<JcahoOrganizationReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<JcahoOrganizationReadOnlyDto>(
                    "[dbo].[get_jcaho]",
                        param: null);
                        
            }
        }

    }
}
