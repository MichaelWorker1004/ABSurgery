using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class ExamScoreReadOnlyDalTests : TestBase<string>
    {
        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examination_scores]";
            var expectedExaminerUserId = Create<int>();
            var expectedParams =
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ExamScoreReadOnlyDto>());
        
            var sut = new ExamScoreReadOnlyDal(sqlManager);
            await sut.GetByIdAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ExamScoreReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ExamScoreReadOnlyDal(sqlManager);
            var result = await sut.GetByIdAsync();
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
