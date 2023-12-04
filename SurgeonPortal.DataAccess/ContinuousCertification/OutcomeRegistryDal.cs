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



        public async Task<OutcomeRegistryDto> GetByUserIdAsync(int userID)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<OutcomeRegistryDto>(
                    "[dbo].[get_outcome_registries]",
                        new
                        {
                            UserID = userID,
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
                        "[dbo].[insert_outcome_registries]",
                            new
                            {
                                UserId = dto.UserId,
                                SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry,
                                RegistryComments = dto.RegistryComments,
                                RegisteredWithACHQC = dto.RegisteredWithACHQC,
                                RegisteredWithCESQIP = dto.RegisteredWithCESQIP,
                                RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP,
                                RegisteredWithABA = dto.RegisteredWithABA,
                                RegisteredWithASBS = dto.RegisteredWithASBS,
                                RegisteredWithMSQC = dto.RegisteredWithMSQC,
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
                                RegisteredWithSSR = dto.RegisteredWithSSR,
                                UserConfirmed = dto.UserConfirmed,
                                UserConfirmedDateUtc = dto.UserConfirmedDateUtc,
                                LastUpdatedByUserId = dto.LastUpdatedByUserId,
                                CreatedByUserId = dto.CreatedByUserId,
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
                    "[dbo].[update_outcome_registries]",
                        new
                        {
                            UserId = dto.UserId,
                            SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry,
                            RegistryComments = dto.RegistryComments,
                            RegisteredWithACHQC = dto.RegisteredWithACHQC,
                            RegisteredWithCESQIP = dto.RegisteredWithCESQIP,
                            RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP,
                            RegisteredWithABA = dto.RegisteredWithABA,
                            RegisteredWithASBS = dto.RegisteredWithASBS,
                            RegisteredWithMSQC = dto.RegisteredWithMSQC,
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
                            RegisteredWithSSR = dto.RegisteredWithSSR,
                            UserConfirmed = dto.UserConfirmed,
                            UserConfirmedDateUtc = dto.UserConfirmedDateUtc,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

    }
}
