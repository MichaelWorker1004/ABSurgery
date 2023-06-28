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
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseCommentDto>(
                    "[dbo].[ins_user_case_comments]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            CaseContentId = dto.CaseContentId,
                            Comments = dto.Comments,
                            CreatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
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
                            LastUpdatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
