using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class UserTokenDalTests : TestBase<int>
    {
        #region GetActiveAsync
        
        [Test]
        public async Task GetActiveAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_usertoken_getactive]";
            var expectedToken = Create<string>();
            var expectedParams =
                new
                {
                    Token = expectedToken,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<UserTokenDto>());
        
            var sut = new UserTokenDal(sqlManager);
            await sut.GetActiveAsync(expectedToken);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetActiveAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserTokenDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserTokenDal(sqlManager);
            var result = await sut.GetActiveAsync(Create<string>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[insert_usertoken]";
            var expectedDto = Create<UserTokenDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserTokenDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    Token = expectedDto.Token,
                    ExpiresAt = expectedDto.ExpiresAt,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserTokenDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserTokenDal(sqlManager);
            var result = await sut.InsertAsync(Create<UserTokenDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
