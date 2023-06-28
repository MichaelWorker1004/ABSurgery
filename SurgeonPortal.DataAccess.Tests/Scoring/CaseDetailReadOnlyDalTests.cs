using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class CaseDetailReadOnlyDalTests : TestBase<string>
    {
        #region GetByCaseHeaderIdAsync
        
        [Test]
        public async Task GetByCaseHeaderIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_case_details_by_id]";
            var expectedCaseHeaderId = Create<int>();
            var expectedExaminerUserId = Create<int>();
            var expectedParams =
                new
                {
                    CaseHeaderId = expectedCaseHeaderId,
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<CaseDetailReadOnlyDto>());
        
            var sut = new CaseDetailReadOnlyDal(sqlManager);
            await sut.GetByCaseHeaderIdAsync(expectedCaseHeaderId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByCaseHeaderIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<CaseDetailReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new CaseDetailReadOnlyDal(sqlManager);
            var result = await sut.GetByCaseHeaderIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
