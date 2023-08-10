using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class ExamTitleReadOnlyDal : SqlServerDalBase, IExamTitleReadOnlyDal
    {
        public ExamTitleReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ExamTitleReadOnlyDto> GetByExamIdAsync(int examId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ExamTitleReadOnlyDto>(
                    "[dbo].[get_exam_title_byExamId]",
                        new
                        {
                            ExamId = examId,
                        });
                        
            }
        }

    }
}
