using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ReferenceLetterAltRoleReadOnlyDal : SqlServerDalBase, IReferenceLetterAltRoleReadOnlyDal
    {
        public ReferenceLetterAltRoleReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ReferenceLetterAltRoleReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ReferenceLetterAltRoleReadOnlyDto>(
                    "[dbo].[get_regLet_altrole_picklist]",
                        param: null);
                        
            }
        }

    }
}
