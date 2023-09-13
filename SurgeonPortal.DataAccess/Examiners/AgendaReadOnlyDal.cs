using SurgeonPortal.DataAccess.Contracts.Examiners;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examiners
{
    public class AgendaReadOnlyDal : SqlServerDalBase, IAgendaReadOnlyDal
    {
        public AgendaReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<AgendaReadOnlyDto> GetByExamHeaderIdAsync(int examHeaderId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AgendaReadOnlyDto>(
                    "[dbo].[get_examiner_agenda]",
                        new
                        {
                            ExaminerUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            ExamHeaderId = examHeaderId,
                        });
                        
            }
        }

    }
}
