using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class QualifyingExamReadOnlyDal : SqlServerDalBase, IQualifyingExamReadOnlyDal
    {
        public QualifyingExamReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<QualifyingExamReadOnlyDto> GetAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<QualifyingExamReadOnlyDto>(
                    "[dbo].[get_current_qualifying_exam]",
                        param: null);
                        
            }
        }

    }
}
