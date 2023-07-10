using SurgeonPortal.DataAccess.Contracts.Documents;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Documents
{
    public class DocumentDal : SqlServerDalBase, IDocumentDal
    {
        public DocumentDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(DocumentDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[delete_userdocument_byid]",
                        new
                        {
                            Id = dto.Id,
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<DocumentDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<DocumentDto>(
                    "[dbo].[get_document_byid]",
                        new
                        {
                            Id = id,
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<DocumentDto> InsertAsync(DocumentDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<DocumentDto>(
                        "[dbo].[ins_userdocument]",
                            new
                            {
                                UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                StreamId = dto.StreamId,
                                DocumentName = dto.DocumentName,
                                DocumentTypeId = dto.DocumentTypeId,
                                InternalViewOnly = dto.InternalViewOnly,
                                CreatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("Document");
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
