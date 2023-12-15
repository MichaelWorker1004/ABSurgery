using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class ReferenceLetterReadOnlyDal : SqlServerDalBase, IReferenceLetterReadOnlyDal
    {
        public ReferenceLetterReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<IEnumerable<ReferenceLetterReadOnlyDto>> GetAllByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecAsync<ReferenceLetterReadOnlyDto>(
                    "[dbo].[get_cc_refLetters]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

    }
}
