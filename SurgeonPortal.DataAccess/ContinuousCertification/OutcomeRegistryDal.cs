using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class OutcomeRegistryDal : SqlServerDalBase, IOutcomeRegistryDal
    {
        public OutcomeRegistryDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<OutcomeRegistryDto> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<OutcomeRegistryDto>(
                    "[dbo].[get_outcomeregistry_getbyuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<OutcomeRegistryDto> InsertAsync(OutcomeRegistryDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<OutcomeRegistryDto>(
                        "[dbo].[ins_outcomeregistry_getbyuserid]",
                            new
                            {
                                UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry,
                                RegistryComments = dto.RegistryComments,
                                RegisteredWithACHQC = dto.RegisteredWithACHQC,
                                RegisteredWithCESQIP = dto.RegisteredWithCESQIP,
                                RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP,
                                RegisteredWithABA = dto.RegisteredWithABA,
                                RegisteredWithASBS = dto.RegisteredWithASBS,
                                RegisteredWithStatewideCollaboratives = dto.RegisteredWithStatewideCollaboratives,
                                RegisteredWithABMS = dto.RegisteredWithABMS,
                                RegisteredWithNCDB = dto.RegisteredWithNCDB,
                                RegisteredWithRQRS = dto.RegisteredWithRQRS,
                                RegisteredWithNSQIP = dto.RegisteredWithNSQIP,
                                RegisteredWithNTDB = dto.RegisteredWithNTDB,
                                RegisteredWithSTS = dto.RegisteredWithSTS,
                                RegisteredWithTQIP = dto.RegisteredWithTQIP,
                                RegisteredWithUNOS = dto.RegisteredWithUNOS,
                                RegisteredWithNCDR = dto.RegisteredWithNCDR,
                                RegisteredWithSVS = dto.RegisteredWithSVS,
                                RegisteredWithELSO = dto.RegisteredWithELSO,
                                UserConfirmed = dto.UserConfirmed,
                                UserConfirmedDateUtc = dto.UserConfirmedDateUtc,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("OutcomeRegistry");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<OutcomeRegistryDto> UpdateAsync(OutcomeRegistryDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<OutcomeRegistryDto>(
                    "[dbo].[update_outcomeregistry_getbyuserid]",
                        new
                        {
                            SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry,
                            RegistryComments = dto.RegistryComments,
                            RegisteredWithACHQC = dto.RegisteredWithACHQC,
                            RegisteredWithCESQIP = dto.RegisteredWithCESQIP,
                            RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP,
                            RegisteredWithABA = dto.RegisteredWithABA,
                            RegisteredWithASBS = dto.RegisteredWithASBS,
                            RegisteredWithStatewideCollaboratives = dto.RegisteredWithStatewideCollaboratives,
                            RegisteredWithABMS = dto.RegisteredWithABMS,
                            RegisteredWithNCDB = dto.RegisteredWithNCDB,
                            RegisteredWithRQRS = dto.RegisteredWithRQRS,
                            RegisteredWithNSQIP = dto.RegisteredWithNSQIP,
                            RegisteredWithNTDB = dto.RegisteredWithNTDB,
                            RegisteredWithSTS = dto.RegisteredWithSTS,
                            RegisteredWithTQIP = dto.RegisteredWithTQIP,
                            RegisteredWithUNOS = dto.RegisteredWithUNOS,
                            RegisteredWithNCDR = dto.RegisteredWithNCDR,
                            RegisteredWithSVS = dto.RegisteredWithSVS,
                            RegisteredWithELSO = dto.RegisteredWithELSO,
                            UserConfirmed = dto.UserConfirmed,
                            UserConfirmedDateUtc = dto.UserConfirmedDateUtc,
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
