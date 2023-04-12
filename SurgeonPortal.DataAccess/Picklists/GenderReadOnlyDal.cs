using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class GenderReadOnlyDal : SqlServerDalBase, IGenderReadOnlyDal
    {
        public GenderReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<GenderReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<GenderReadOnlyDto>(
                    "[dbo].[get_picklist_genders_all]",
                        param: null);
                        
            }
        }

    }
}
