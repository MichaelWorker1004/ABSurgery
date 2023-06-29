using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.DataAccess.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Picklists
{
	public class ScoringSessionReadOnlyDalTests : TestBase<string>
    {
        #region GetByExaminerIdAsync
        
        [Test]
        public async Task GetByExaminerIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_day_session_picklist]";
            var expectedExaminerUserId = Create<int>();
            var expectedExamHeaderId = Create<int>();
            var expectedParams =
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    ExamHeaderId = expectedExamHeaderId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ScoringSessionReadOnlyDto>());
        
            var sut = new ScoringSessionReadOnlyDal(sqlManager);
            await sut.GetByExaminerIdAsync(expectedExamHeaderId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExaminerIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ScoringSessionReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ScoringSessionReadOnlyDal(sqlManager);
            var result = await sut.GetByExaminerIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}