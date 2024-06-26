using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class StateReadOnlyDal : SqlServerDalBase, IStateReadOnlyDal
    {
        public StateReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<StateReadOnlyDto>> GetByCountryAsync(string countryCode)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<StateReadOnlyDto>(
                    "[dbo].[get_picklist_states_bycountry]",
                        new
                        {
                            CountryCode = countryCode,
                        });
                        
            }
        }

    }
}
