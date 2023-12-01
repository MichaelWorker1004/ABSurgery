using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.ContinuousCertification;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.ContinuousCertification
{
	public class DashboardStatusReadOnlyDalTests : TestBase<int>
    {
        #region GetAllByUserIdAsync
        
        [Test]
        public async Task GetAllByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_all_status]";
            var expectedUserId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<DashboardStatusReadOnlyDto>());
        
            var sut = new DashboardStatusReadOnlyDal(sqlManager);
            await sut.GetAllByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<DashboardStatusReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new DashboardStatusReadOnlyDal(sqlManager);
            var result = await sut.GetAllByUserIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
