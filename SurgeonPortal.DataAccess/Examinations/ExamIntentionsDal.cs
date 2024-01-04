using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class ExamIntentionsDal : SqlServerDalBase, IExamIntentionsDal
    {
        public ExamIntentionsDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ExamIntentionsDto> GetByExamIdAsync(
            int userId,
            int examId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ExamIntentionsDto>(
                    "[dbo].[get_exam_intentions]",
                        new
                        {
                            UserId = userId,
                            ExamId = examId,
                        });
                        
            }
        }

        public async Task<ExamIntentionsDto> InsertAsync(ExamIntentionsDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<ExamIntentionsDto>(
                        "[dbo].[ins_exam_intentions]",
                            new
                            {
                                UserId = dto.UserId,
                                ExamId = dto.ExamId,
                                Intention = dto.Intention,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("ExamIntentions");
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
