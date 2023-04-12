using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class RaceReadOnlyDal : SqlServerDalBase, IRaceReadOnlyDal
    {
        public RaceReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<RaceReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<RaceReadOnlyDto>(
                    "[dbo].[get_picklist_races_all]",
                        param: null);
                        
            }
        }

    }
}
