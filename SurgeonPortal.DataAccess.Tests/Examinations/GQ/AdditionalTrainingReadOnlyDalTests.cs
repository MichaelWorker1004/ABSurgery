using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.DataAccess.Examinations.GQ;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations.GQ
{
	public class AdditionalTrainingReadOnlyDalTests : TestBase<string>
    {
        #region GetAllByUserIdAsync
        
        [Test]
        public async Task GetAllByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_additionaltrainingreadonly_allbyuserid]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<AdditionalTrainingReadOnlyDto>());
        
            var sut = new AdditionalTrainingReadOnlyDal(sqlManager);
            await sut.GetAllByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<AdditionalTrainingReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new AdditionalTrainingReadOnlyDal(sqlManager);
            var result = await sut.GetAllByUserIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
