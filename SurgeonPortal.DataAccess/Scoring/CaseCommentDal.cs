using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class CaseCommentDal : SqlServerDalBase, ICaseCommentDal
    {
        public CaseCommentDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(CaseCommentDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[delete_case_comments_byid]",
                        new
                        {
                            Id = dto.Id,
                        });
                        
            }
        }

        public async Task<CaseCommentDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseCommentDto>(
                    "[dbo].[get_case_comments_byid]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<CaseCommentDto> InsertAsync(CaseCommentDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<CaseCommentDto>(
                        "[dbo].[ins_user_case_comments]",
                            new
                            {
                                UserId = dto.UserId,
                                CaseContentId = dto.CaseContentId,
                                Comments = dto.Comments,
                                CreatedByUserId = dto.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("CaseComment");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<CaseCommentDto> UpdateAsync(CaseCommentDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseCommentDto>(
                    "[dbo].[update_case_comments]",
                        new
                        {
                            Id = dto.Id,
                            CaseContentId = dto.CaseContentId,
                            Comments = dto.Comments,
                            LastUpdatedByUserId = dto.UserId,
                        });
                        
            }
        }

    }
}
