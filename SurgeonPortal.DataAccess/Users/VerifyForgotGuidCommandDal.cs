using SurgeonPortal.DataAccess.Contracts.Users;
using System;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class VerifyForgotGuidCommandDal : SqlServerDalBase, IVerifyForgotGuidCommandDal
    {
        public VerifyForgotGuidCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<VerifyForgotGuidCommandDto> VerifyForgotPasswordGuidAsync(Guid resetGUID)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<VerifyForgotGuidCommandDto>(
                    "[dbo].[get_guid_status]",
                        new
                        {
                            ResetGUID = resetGUID,
                        });
                        
            }
        }

    }
}
