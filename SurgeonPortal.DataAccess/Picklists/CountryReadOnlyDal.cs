using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class CountryReadOnlyDal : SqlServerDalBase, ICountryReadOnlyDal
    {
        public CountryReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<CountryReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CountryReadOnlyDto>(
                    "[dbo].[get_picklist_country_all]",
                        param: null);
                        
            }
        }

    }
}
