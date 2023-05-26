using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.Shared;
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
                            UserId = IdentityHelper.UserId,
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
                            UserId = IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<DocumentDto> InsertAsync(DocumentDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<DocumentDto>(
                    "[dbo].[ins_userdocument]",
                        new
                        {
                            UserId = dto.UserId,
                            StreamId = dto.StreamId,
                            DocumentName = dto.DocumentName,
                            DocumentTypeId = dto.DocumentTypeId,
                            InternalViewOnly = dto.InternalViewOnly,
                            CreatedByUserId = dto.CreatedByUserId,
                        });
                        
            }
        }

        public async Task<DocumentDto> UpdateAsync(DocumentDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<DocumentDto>(
                    "[dbo].[update_userdocument]",
                        new
                        {
                            Id = dto.Id,
                            UserId = dto.UserId,
                            StreamId = dto.StreamId,
                            DocumentName = dto.DocumentName,
                            DocumentTypeId = dto.DocumentTypeId,
                            InternalViewOnly = dto.InternalViewOnly,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

    }
}
