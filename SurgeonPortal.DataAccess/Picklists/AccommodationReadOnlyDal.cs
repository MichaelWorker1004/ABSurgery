using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class AccommodationReadOnlyDal : SqlServerDalBase, IAccommodationReadOnlyDal
    {
        public AccommodationReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<AccommodationReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<AccommodationReadOnlyDto>(
                    "[dbo].[get_accommodation_picklist]",
                        param: null);
                        
            }
        }

    }
}
