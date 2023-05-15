using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class TrainingTypeReadOnlyDal : SqlServerDalBase, ITrainingTypeReadOnlyDal
    {
        public TrainingTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<TrainingTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<TrainingTypeReadOnlyDto>(
                    "[dbo].[get_training_type]",
                        param: null);
                        
            }
        }

    }
}
