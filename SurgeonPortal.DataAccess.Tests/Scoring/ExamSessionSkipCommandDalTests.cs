using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class ExamSessionSkipCommandDalTests : TestBase<int>
    {
        #region SkipExamSessionAsync
        
        [Test]
        public async Task SkipExamSessionAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_skip_exam]";
            var expectedExamScheduleId = Create<int>();
            var expectedExaminerUserId = Create<int>();
            var expectedParams = 
                new
                {
                    ExamScheduleId = expectedExamScheduleId,
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ExamSessionSkipCommandDto>());
        
            var sut = new ExamSessionSkipCommandDal(sqlManager);
            await sut.SkipExamSessionAsync(
                expectedExamScheduleId,
                expectedExaminerUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
