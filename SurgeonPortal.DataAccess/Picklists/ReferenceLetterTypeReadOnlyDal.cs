using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ReferenceLetterTypeReadOnlyDal : SqlServerDalBase, IReferenceLetterTypeReadOnlyDal
    {
        public ReferenceLetterTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ReferenceLetterTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ReferenceLetterTypeReadOnlyDto>(
                    "[dbo].[get_refLet_let_type]",
                        param: null);
                        
            }
        }

    }
}
