using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class LanguageReadOnlyDal : SqlServerDalBase, ILanguageReadOnlyDal
    {
        public LanguageReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<LanguageReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<LanguageReadOnlyDto>(
                    "[dbo].[get_picklist_languages_all]",
                        param: null);
                        
            }
        }

    }
}
