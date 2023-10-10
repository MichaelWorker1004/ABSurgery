using SurgeonPortal.DataAccess.Contracts.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Documents
{
    public class DocumentReadOnlyDal : SqlServerDalBase, IDocumentReadOnlyDal
    {
        public DocumentReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<DocumentReadOnlyDto>> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<DocumentReadOnlyDto>(
                    "[dbo].[get_documents_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
