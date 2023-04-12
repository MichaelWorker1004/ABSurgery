using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class EthnicityReadOnlyDal : SqlServerDalBase, IEthnicityReadOnlyDal
    {
        public EthnicityReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<EthnicityReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<EthnicityReadOnlyDto>(
                    "[dbo].[get_picklists_ethnicities_all]",
                        param: null);
                        
            }
        }

    }
}
