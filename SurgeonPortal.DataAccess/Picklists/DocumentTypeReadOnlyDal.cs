using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class DocumentTypeReadOnlyDal : SqlServerDalBase, IDocumentTypeReadOnlyDal
    {
        public DocumentTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<DocumentTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<DocumentTypeReadOnlyDto>(
                    "[dbo].[get_document_types]",
                        param: null);
                        
            }
        }

    }
}
