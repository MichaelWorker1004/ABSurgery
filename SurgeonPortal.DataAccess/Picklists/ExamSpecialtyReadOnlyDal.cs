using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ExamSpecialtyReadOnlyDal : SqlServerDalBase, IExamSpecialtyReadOnlyDal
    {
        public ExamSpecialtyReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ExamSpecialtyReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamSpecialtyReadOnlyDto>(
                    "[dbo].[get_examspecialties]",
                        param: null);
                        
            }
        }

    }
}
