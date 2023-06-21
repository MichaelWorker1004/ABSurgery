using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class OrganizationTypeReadOnlyDal : SqlServerDalBase, IOrganizationTypeReadOnlyDal
    {
        public OrganizationTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<OrganizationTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<OrganizationTypeReadOnlyDto>(
                    "[dbo].[get_organization_type]",
                        param: null);
                        
            }
        }

    }
}
