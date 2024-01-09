using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class RegistrationCompleteCommandDal : SqlServerDalBase, IRegistrationCompleteCommandDal
    {
        public RegistrationCompleteCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task CompleteRegistrationAsync(
            int userId,
            int examHeaderId)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[update_user_exam_registration_complete]",
                        new
                        {
                            UserId = userId,
                            ExamHeaderId = examHeaderId,
                        });
                        
            }
        }

    }
}
