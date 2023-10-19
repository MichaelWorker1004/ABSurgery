using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class CaseFeedbackDal : SqlServerDalBase, ICaseFeedbackDal
    {
        public CaseFeedbackDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(CaseFeedbackDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[delete_case_feedback_byid]",
                        new
                        {
                            Id = dto.Id,
                        });
                        
            }
        }

        public async Task<CaseFeedbackDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseFeedbackDto>(
                    "[dbo].[get_case_feedback_byid]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<CaseFeedbackDto> InsertAsync(CaseFeedbackDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<CaseFeedbackDto>(
                        "[dbo].[ins_case_feedback]",
                            new
                            {
                                UserId = dto.UserId,
                                CaseHeaderId = dto.CaseHeaderId,
                                Feedback = dto.Feedback,
                                CreatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("CaseFeedback");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<CaseFeedbackDto> UpdateAsync(CaseFeedbackDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseFeedbackDto>(
                    "[dbo].[update_case_feedback]",
                        new
                        {
                            Id = dto.Id,
                            CaseHeaderId = dto.CaseHeaderId,
                            Feedback = dto.Feedback,
                            LastUpdatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
