using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class CaseRosterReadOnlyDalTests : TestBase<string>
    {
        #region GetByScheduleIdAsync
        
        [Test]
        public async Task GetByScheduleIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_toc_case_list]";
            var expectedScheduleId1 = Create<int>();
            var expectedScheduleId2 = Create<int?>();
            var expectedParams =
                new
                {
                    ScheduleId1 = expectedScheduleId1,
                    ScheduleId2 = expectedScheduleId2,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<CaseRosterReadOnlyDto>());
        
            var sut = new CaseRosterReadOnlyDal(sqlManager);
            await sut.GetByScheduleIdAsync(
                expectedScheduleId1,
                expectedScheduleId2);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByScheduleIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<CaseRosterReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new CaseRosterReadOnlyDal(sqlManager);
            var result = await sut.GetByScheduleIdAsync(
                Create<int>(),
                Create<int?>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
