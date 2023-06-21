using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ProfessionalStanding
{
    public class UserAppointmentReadOnlyDal : SqlServerDalBase, IUserAppointmentReadOnlyDal
    {
        public UserAppointmentReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<UserAppointmentReadOnlyDto>> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<UserAppointmentReadOnlyDto>(
                    "[dbo].[get_userhospappts_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
