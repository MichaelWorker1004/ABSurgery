using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class CaseFeedbackReadOnlyDalTests : TestBase<int>
    {
        #region GetByExaminerIdAsync
        
        [Test]
        public async Task GetByExaminerIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_case_feedback_by_examinerId]";
            var expectedExaminerUserId = Create<int>();
            var expectedCaseHeaderId = Create<int>();
            var expectedParams =
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    CaseHeaderId = expectedCaseHeaderId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<CaseFeedbackReadOnlyDto>());
        
            var sut = new CaseFeedbackReadOnlyDal(sqlManager);
            await sut.GetByExaminerIdAsync(
                expectedExaminerUserId,
                expectedCaseHeaderId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExaminerIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseFeedbackReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseFeedbackReadOnlyDal(sqlManager);
            var result = await sut.GetByExaminerIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
