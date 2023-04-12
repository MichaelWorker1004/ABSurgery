using SurgeonPortal.DataAccess.Contracts.Surgeons;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Surgeons
{
    public class CertificationReadOnlyDal : SqlServerDalBase, ICertificationReadOnlyDal
    {
        public CertificationReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<CertificationReadOnlyDto> GetByAbsIdAsync(string absId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CertificationReadOnlyDto>(
                    "[dbo].[get_user_certifications]",
                        new
                        {
                            AbsId = absId,
                        });
                        
            }
        }

    }
}
