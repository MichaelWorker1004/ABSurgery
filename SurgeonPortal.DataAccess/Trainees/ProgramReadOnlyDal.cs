using SurgeonPortal.DataAccess.Contracts.Trainees;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Trainees
{
    public class ProgramReadOnlyDal : SqlServerDalBase, IProgramReadOnlyDal
    {
        public ProgramReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ProgramReadOnlyDto> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ProgramReadOnlyDto>(
                    "[dbo].[get_user_programs]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
