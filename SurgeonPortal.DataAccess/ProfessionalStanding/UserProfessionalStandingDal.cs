using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ProfessionalStanding
{
    public class UserProfessionalStandingDal : SqlServerDalBase, IUserProfessionalStandingDal
    {
        public UserProfessionalStandingDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<UserProfessionalStandingDto> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserProfessionalStandingDto>(
                    "[dbo].[get_user_professional_standing_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<UserProfessionalStandingDto> InsertAsync(UserProfessionalStandingDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<UserProfessionalStandingDto>(
                        "[dbo].[ins_user_sanctions]",
                            new
                            {
                                UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                PrimaryPracticeID = dto.PrimaryPracticeId,
                                OrganizationTypeId = dto.OrganizationTypeId,
                                ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges,
                                ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities,
                                CreatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                LastUpdatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("UserProfessionalStanding");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<UserProfessionalStandingDto> UpdateAsync(UserProfessionalStandingDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserProfessionalStandingDto>(
                    "[dbo].[update_user_professional_standing_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            PrimaryPracticeID = dto.PrimaryPracticeId,
                            OrganizationTypeId = dto.OrganizationTypeId,
                            ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges,
                            ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities,
                            LastUpdatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
