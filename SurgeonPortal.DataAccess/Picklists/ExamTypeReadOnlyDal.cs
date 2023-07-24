using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ExamTypeReadOnlyDal : SqlServerDalBase, IExamTypeReadOnlyDal
    {
        public ExamTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ExamTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamTypeReadOnlyDto>(
                    "[dbo].[get_examtypes]",
                        param: null);
                        
            }
        }

    }
}
