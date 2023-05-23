using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ResidencyProgramReadOnlyDal : SqlServerDalBase, IResidencyProgramReadOnlyDal
    {
        public ResidencyProgramReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ResidencyProgramReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ResidencyProgramReadOnlyDto>(
                    "[dbo].[get_residency_program]",
                        param: null);
                        
            }
        }

    }
}
