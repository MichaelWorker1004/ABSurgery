using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class UserCredentialDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_account]";
            var expectedUserId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<UserCredentialDto>());
        
            var sut = new UserCredentialDal(sqlManager);
            await sut.GetByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserCredentialDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserCredentialDal(sqlManager);
            var result = await sut.GetByUserIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[upd_user_account]";
            var expectedDto = Create<UserCredentialDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserCredentialDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    EmailAddress = expectedDto.EmailAddress,
                    Password = expectedDto.Password,
                    LastUpdatedByUserId = expectedDto.LastUpdatedByUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserCredentialDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserCredentialDal(sqlManager);
            var result = await sut.UpdateAsync(Create<UserCredentialDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
