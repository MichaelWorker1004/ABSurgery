using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class FellowshipProgramReadOnlyDal : SqlServerDalBase, IFellowshipProgramReadOnlyDal
    {
        public FellowshipProgramReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<FellowshipProgramReadOnlyDto>> GetAllAsync(string fellowshipType)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<FellowshipProgramReadOnlyDto>(
                    "[dbo].[get_fellowship_program]",
                        new
                        {
                            FellowshipType = fellowshipType,
                        });
                        
            }
        }

    }
}
