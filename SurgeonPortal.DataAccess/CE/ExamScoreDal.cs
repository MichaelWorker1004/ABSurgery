using SurgeonPortal.DataAccess.Contracts.CE;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.CE
{
    public class ExamScoreDal : SqlServerDalBase, IExamScoreDal
    {
        public ExamScoreDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ExamScoreDto> GetByIdAsync(int examScheduleScoreId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ExamScoreDto>(
                    "[dbo].[get_exam_schedule_scores]",
                        new
                        {
                            ExamScheduleScoreId = examScheduleScoreId,
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<ExamScoreDto> InsertAsync(ExamScoreDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<ExamScoreDto>(
                        "[dbo].[ins_exam_schedule_scores]",
                            new
                            {
                                ExamScheduleId = dto.ExamScheduleId,
                                ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("ExamScore");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ExamScoreDto> UpdateAsync(ExamScoreDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ExamScoreDto>(
                    "[dbo].[update_exam_schedule_scores]",
                        new
                        {
                            ExamScheduleScoreId = dto.ExamScheduleScoreId,
                            ExaminerScore = dto.ExaminerScore,
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
