using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class RosterReadOnlyDalTests : TestBase<int>
    {
        #region GetByExaminationHeaderIdAsync
        
        [Test]
        public async Task GetByExaminationHeaderIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examination_scores]";
            var expectedExaminerUserId = Create<int>();
            var expectedExamHeaderId = Create<int>();
            var expectedParams = 
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    ExamHeaderId = expectedExamHeaderId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<RosterReadOnlyDto>());
        
            var sut = new RosterReadOnlyDal(sqlManager);
            await sut.GetByExaminationHeaderIdAsync(
                expectedExaminerUserId,
                expectedExamHeaderId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExaminationHeaderIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<RosterReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new RosterReadOnlyDal(sqlManager);
            var result = await sut.GetByExaminationHeaderIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
