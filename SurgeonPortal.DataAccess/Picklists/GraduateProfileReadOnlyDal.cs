using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class GraduateProfileReadOnlyDal : SqlServerDalBase, IGraduateProfileReadOnlyDal
    {
        public GraduateProfileReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<GraduateProfileReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<GraduateProfileReadOnlyDto>(
                    "[dbo].[get_graduate_profile]",
                        param: null);
                        
            }
        }

    }
}
