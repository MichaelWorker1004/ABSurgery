using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class RequirementsReadOnlyDal : SqlServerDalBase, IRequirementsReadOnlyDal
    {
        public RequirementsReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<RequirementsReadOnlyDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<RequirementsReadOnlyDto>(
                    "[dbo].[get_user_meeting_cc_requirements_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
