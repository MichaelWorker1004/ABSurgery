using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class FellowshipTypeReadOnlyDal : SqlServerDalBase, IFellowshipTypeReadOnlyDal
    {
        public FellowshipTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<FellowshipTypeReadOnlyDto>> GetAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<FellowshipTypeReadOnlyDto>(
                    "[dbo].[get_fellowship_types]",
                        param: null);
                        
            }
        }

    }
}
