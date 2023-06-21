using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class PracticeTypeReadOnlyDal : SqlServerDalBase, IPracticeTypeReadOnlyDal
    {
        public PracticeTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<PracticeTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<PracticeTypeReadOnlyDto>(
                    "[dbo].[get_practice_types]",
                        param: null);
                        
            }
        }

    }
}
