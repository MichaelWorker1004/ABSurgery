using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class AdvancedTrainingReadOnlyDal : SqlServerDalBase, IAdvancedTrainingReadOnlyDal
    {
        public AdvancedTrainingReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<AdvancedTrainingReadOnlyDto>> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<AdvancedTrainingReadOnlyDto>(
                    "[dbo].[get_advanced_training_by_userid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
