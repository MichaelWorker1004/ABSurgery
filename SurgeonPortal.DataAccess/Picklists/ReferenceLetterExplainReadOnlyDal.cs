using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ReferenceLetterExplainReadOnlyDal : SqlServerDalBase, IReferenceLetterExplainReadOnlyDal
    {
        public ReferenceLetterExplainReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ReferenceLetterExplainReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ReferenceLetterExplainReadOnlyDto>(
                    "[dbo].[get_refLet_explain_picklist]",
                        param: null);
                        
            }
        }

    }
}
