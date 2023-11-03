using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class UserClaimReadOnlyDalTests : TestBase<int>
    {
        #region GetByIdsAsync
        
        [Test]
        public async Task GetByIdsAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_claims]";
            var expectedUserId = Create<int>();
            var expectedApplicationId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                    ApplicationId = expectedApplicationId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<UserClaimReadOnlyDto>());
        
            var sut = new UserClaimReadOnlyDal(sqlManager);
            await sut.GetByIdsAsync(
                expectedUserId,
                expectedApplicationId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdsAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<UserClaimReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new UserClaimReadOnlyDal(sqlManager);
            var result = await sut.GetByIdsAsync(
                Create<int>(),
                Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
