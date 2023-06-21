using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class PrimaryPracticeReadOnlyDal : SqlServerDalBase, IPrimaryPracticeReadOnlyDal
    {
        public PrimaryPracticeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<PrimaryPracticeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<PrimaryPracticeReadOnlyDto>(
                    "[dbo].[get_primary_practice]",
                        param: null);
                        
            }
        }

    }
}
