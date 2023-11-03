using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class CaseScoreReadOnlyDalTests : TestBase<int>
    {
        #region GetByExamScheduleIdAsync
        
        [Test]
        public async Task GetByExamScheduleIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examinerscores_byexamscheduleId]";
            var expectedExaminerUserId = Create<int>();
            var expectedExamScheduleId = Create<int>();
            var expectedParams = 
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    ExamScheduleId = expectedExamScheduleId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<CaseScoreReadOnlyDto>());
        
            var sut = new CaseScoreReadOnlyDal(sqlManager);
            await sut.GetByExamScheduleIdAsync(
                expectedExaminerUserId,
                expectedExamScheduleId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamScheduleIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<CaseScoreReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new CaseScoreReadOnlyDal(sqlManager);
            var result = await sut.GetByExamScheduleIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
