using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ClinicalLevelReadOnlyDal : SqlServerDalBase, IClinicalLevelReadOnlyDal
    {
        public ClinicalLevelReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ClinicalLevelReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ClinicalLevelReadOnlyDto>(
                    "[dbo].[get_clinicallevel]",
                        param: null);
                        
            }
        }

    }
}
