using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class UserProfileDalTests : TestBase<string>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_profile_byuserid]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<UserProfileDto>());
        
            var sut = new UserProfileDal(sqlManager);
            await sut.GetByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserProfileDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserProfileDal(sqlManager);
            var result = await sut.GetByUserIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[insert_user_profile]";
            var expectedDto = Create<UserProfileDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserProfileDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    FirstName = expectedDto.FirstName,
                    MiddleName = expectedDto.MiddleName,
                    LastName = expectedDto.LastName,
                    Suffix = expectedDto.Suffix,
                    DisplayName = expectedDto.DisplayName,
                    OfficePhoneNumber = expectedDto.OfficePhoneNumber,
                    MobilePhoneNumber = expectedDto.MobilePhoneNumber,
                    BirthCity = expectedDto.BirthCity,
                    BirthState = expectedDto.BirthState,
                    BirthCountry = expectedDto.BirthCountry,
                    CountryCitizenship = expectedDto.CountryCitizenship,
                    ABSId = expectedDto.ABSId,
                    NPI = expectedDto.NPI,
                    GenderId = expectedDto.GenderId,
                    BirthDate = expectedDto.BirthDate,
                    Race = expectedDto.Race,
                    Ethnicity = expectedDto.Ethnicity,
                    FirstLanguageId = expectedDto.FirstLanguageId,
                    BestLanguageId = expectedDto.BestLanguageId,
                    ReceiveComms = expectedDto.ReceiveComms,
                    UserConfirmed = expectedDto.UserConfirmed,
                    UserConfirmedDate = expectedDto.UserConfirmedDate,
                    Street1 = expectedDto.Street1,
                    Street2 = expectedDto.Street2,
                    City = expectedDto.City,
                    State = expectedDto.State,
                    ZipCode = expectedDto.ZipCode,
                    Country = expectedDto.Country,
                    CreatedByUserId = expectedDto.CreatedByUserId,
                    CreatedAtUtc = expectedDto.CreatedAtUtc,
                    LastUpdatedAtUtc = expectedDto.LastUpdatedAtUtc,
                    LastUpdatedByUserId = expectedDto.LastUpdatedByUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserProfileDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserProfileDal(sqlManager);
            var result = await sut.InsertAsync(Create<UserProfileDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_user_profile]";
            var expectedDto = Create<UserProfileDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserProfileDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    UserProfileId = expectedDto.UserProfileId,
                    UserId = expectedDto.UserId,
                    FirstName = expectedDto.FirstName,
                    MiddleName = expectedDto.MiddleName,
                    LastName = expectedDto.LastName,
                    Suffix = expectedDto.Suffix,
                    DisplayName = expectedDto.DisplayName,
                    OfficePhoneNumber = expectedDto.OfficePhoneNumber,
                    MobilePhoneNumber = expectedDto.MobilePhoneNumber,
                    BirthCity = expectedDto.BirthCity,
                    BirthState = expectedDto.BirthState,
                    BirthCountry = expectedDto.BirthCountry,
                    CountryCitizenship = expectedDto.CountryCitizenship,
                    ABSId = expectedDto.ABSId,
                    NPI = expectedDto.NPI,
                    GenderId = expectedDto.GenderId,
                    BirthDate = expectedDto.BirthDate,
                    Race = expectedDto.Race,
                    Ethnicity = expectedDto.Ethnicity,
                    FirstLanguageId = expectedDto.FirstLanguageId,
                    BestLanguageId = expectedDto.BestLanguageId,
                    ReceiveComms = expectedDto.ReceiveComms,
                    UserConfirmed = expectedDto.UserConfirmed,
                    UserConfirmedDate = expectedDto.UserConfirmedDate,
                    Street1 = expectedDto.Street1,
                    Street2 = expectedDto.Street2,
                    City = expectedDto.City,
                    State = expectedDto.State,
                    ZipCode = expectedDto.ZipCode,
                    Country = expectedDto.Country,
                    CreatedByUserId = expectedDto.CreatedByUserId,
                    CreatedAtUtc = expectedDto.CreatedAtUtc,
                    LastUpdatedAtUtc = expectedDto.LastUpdatedAtUtc,
                    LastUpdatedByUserId = expectedDto.LastUpdatedByUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserProfileDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserProfileDal(sqlManager);
            var result = await sut.UpdateAsync(Create<UserProfileDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
