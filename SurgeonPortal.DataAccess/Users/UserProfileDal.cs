using SurgeonPortal.DataAccess.Contracts.Users;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Users
{
    public class UserProfileDal : SqlServerDalBase, IUserProfileDal
    {
        public UserProfileDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<UserProfileDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserProfileDto>(
                    "[dbo].[get_user_profile_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

        public async Task<UserProfileDto> InsertAsync(UserProfileDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserProfileDto>(
                    "[dbo].[insert_user_profile]",
                        new
                        {
                            UserId = dto.UserId,
                            FirstName = dto.FirstName,
                            MiddleName = dto.MiddleName,
                            LastName = dto.LastName,
                            Suffix = dto.Suffix,
                            DisplayName = dto.DisplayName,
                            OfficePhoneNumber = dto.OfficePhoneNumber,
                            MobilePhoneNumber = dto.MobilePhoneNumber,
                            BirthCity = dto.BirthCity,
                            BirthState = dto.BirthState,
                            BirthCountry = dto.BirthCountry,
                            CountryCitizenship = dto.CountryCitizenship,
                            ABSId = dto.ABSId,
                            CertificationStatusId = dto.CertificationStatusId,
                            NPI = dto.NPI,
                            Gender = dto.Gender,
                            BirthDate = dto.BirthDate,
                            Race = dto.Race,
                            Ethnicity = dto.Ethnicity,
                            FirstLanguage = dto.FirstLanguage,
                            BestLanguage = dto.BestLanguage,
                            ReceiveComms = dto.ReceiveComms,
                            UserConfirmed = dto.UserConfirmed,
                            UserConfirmedDate = dto.UserConfirmedDate,
                            CreatedByUserId = dto.CreatedByUserId,
                            CreatedAtUtc = dto.CreatedAtUtc,
                            LastUpdatedAtUtc = dto.LastUpdatedAtUtc,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

        public async Task<UserProfileDto> UpdateAsync(UserProfileDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserProfileDto>(
                    "[dbo].[update_user_profile]",
                        new
                        {
                            UserProfileId = dto.UserProfileId,
                            UserId = dto.UserId,
                            FirstName = dto.FirstName,
                            MiddleName = dto.MiddleName,
                            LastName = dto.LastName,
                            Suffix = dto.Suffix,
                            DisplayName = dto.DisplayName,
                            OfficePhoneNumber = dto.OfficePhoneNumber,
                            MobilePhoneNumber = dto.MobilePhoneNumber,
                            BirthCity = dto.BirthCity,
                            BirthState = dto.BirthState,
                            BirthCountry = dto.BirthCountry,
                            CountryCitizenship = dto.CountryCitizenship,
                            ABSId = dto.ABSId,
                            CertificationStatusId = dto.CertificationStatusId,
                            NPI = dto.NPI,
                            Gender = dto.Gender,
                            BirthDate = dto.BirthDate,
                            Race = dto.Race,
                            Ethnicity = dto.Ethnicity,
                            FirstLanguage = dto.FirstLanguage,
                            BestLanguage = dto.BestLanguage,
                            ReceiveComms = dto.ReceiveComms,
                            UserConfirmed = dto.UserConfirmed,
                            UserConfirmedDate = dto.UserConfirmedDate,
                            CreatedByUserId = dto.CreatedByUserId,
                            CreatedAtUtc = dto.CreatedAtUtc,
                            LastUpdatedAtUtc = dto.LastUpdatedAtUtc,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

    }
}
