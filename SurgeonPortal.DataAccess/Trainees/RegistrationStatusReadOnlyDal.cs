using SurgeonPortal.DataAccess.Contracts.Trainees;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Trainees
{
    public class RegistrationStatusReadOnlyDal : SqlServerDalBase, IRegistrationStatusReadOnlyDal
    {
        public RegistrationStatusReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<RegistrationStatusReadOnlyDto> GetByExamCodeAsync(string examCode)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<RegistrationStatusReadOnlyDto>(
                    "[dbo].[get_registration_open]",
                        new
                        {
                            examCode = examCode,
                        });
                        
            }
        }

    }
}
