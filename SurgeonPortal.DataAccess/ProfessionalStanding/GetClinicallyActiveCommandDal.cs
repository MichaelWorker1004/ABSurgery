using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ProfessionalStanding
{
    public class GetClinicallyActiveCommandDal : SqlServerDalBase, IGetClinicallyActiveCommandDal
    {
        public GetClinicallyActiveCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public GetClinicallyActiveCommandDto GetClinicallyActiveByUserId()
        {
            using (var connection = CreateConnection())
            {
                return connection.ExecFirstOrDefault<GetClinicallyActiveCommandDto>(
                    "[dbo].[get_user_clinically_active_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
