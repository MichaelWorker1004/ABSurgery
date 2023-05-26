using SurgeonPortal.DataAccess.Contracts.Picklists;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Picklists
{
    public class CertificateTypeReadOnlyDal : SqlServerDalBase, ICertificateTypeReadOnlyDal
    {
        public CertificateTypeReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<CertificateTypeReadOnlyDto>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<CertificateTypeReadOnlyDto>(
                    "[dbo].[get_certificate_types]",
                        param: null);
                        
            }
        }

    }
}
