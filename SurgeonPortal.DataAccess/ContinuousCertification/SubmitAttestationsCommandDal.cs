using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class SubmitAttestationsCommandDal : SqlServerDalBase, ISubmitAttestationsCommandDal
    {
        public SubmitAttestationsCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task GetExamCasesScoredAsync(
            int userId,
            DateTime sigReceive,
            DateTime certnoticeReceive)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[update_cca_attestation_byuserid]",
                        new
                        {
                            UserId = userId,
                            SigReceive = sigReceive,
                            CertnoticeReceive = certnoticeReceive,
                        });
                        
            }
        }

    }
}
