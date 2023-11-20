using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class FellowshipReadOnlyDal : SqlServerDalBase, IFellowshipReadOnlyDal
    {
        public FellowshipReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<FellowshipReadOnlyDto>> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<FellowshipReadOnlyDto>(
                    "[dbo].[get_userfellowships_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
