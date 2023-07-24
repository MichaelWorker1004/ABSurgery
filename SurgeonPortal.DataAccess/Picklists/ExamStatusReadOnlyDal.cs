using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class ExamStatusReadOnlyDal : SqlServerDalBase, IExamStatusReadOnlyDal
    {
        public ExamStatusReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ExamStatusReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ExamStatusReadOnlyDto>(
                    "[dbo].[get_examstatus]",
                        param: null);
                        
            }
        }

    }
}
