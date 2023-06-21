using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class AppointmentTypeReadOnlyDal : SqlServerDalBase, IAppointmentTypeReadOnlyDal
    {
        public AppointmentTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<AppointmentTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<AppointmentTypeReadOnlyDto>(
                    "[dbo].[get_appointment_types]",
                        param: null);
                        
            }
        }

    }
}
