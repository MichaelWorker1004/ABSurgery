using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations.GQ
{
    public class AdditionalTrainingReadOnlyDal : SqlServerDalBase, IAdditionalTrainingReadOnlyDal
    {
        public AdditionalTrainingReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<AdditionalTrainingReadOnlyDto>> GetAllByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<AdditionalTrainingReadOnlyDto>(
                    "[dbo].[get_additionaltrainingreadonly_allbyuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
