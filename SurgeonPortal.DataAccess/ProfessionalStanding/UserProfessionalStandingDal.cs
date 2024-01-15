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



        public async Task<UserProfessionalStandingDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserProfessionalStandingDto>(
                    "[dbo].[get_user_professional_standing_byuserid]",
                        new
                        {
                            UserId = userId,
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
                        "[dbo].[ins_user_professional_standing]",
                            new
                            {
                                UserId = dto.UserId,
                                ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges,
                                ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities,
                                CreatedByUserId = dto.UserId,
                                LastUpdatedByUserId = dto.UserId,
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
                            UserId = dto.UserId,
                            ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges,
                            ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities,
                            LastUpdatedByUserId = dto.UserId,
                        });
                        
            }
        }

    }
}
