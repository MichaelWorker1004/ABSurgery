using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ClinicalActivityReadOnlyDal : SqlServerDalBase, IClinicalActivityReadOnlyDal
    {
        public ClinicalActivityReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ClinicalActivityReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ClinicalActivityReadOnlyDto>(
                    "[dbo].[get_clinicalactivity]",
                        param: null);
                        
            }
        }

    }
}
