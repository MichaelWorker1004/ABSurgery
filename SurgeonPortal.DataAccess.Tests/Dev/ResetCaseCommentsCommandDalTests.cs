using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Dev;
using SurgeonPortal.DataAccess.Dev;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Dev
{
	public class ResetCaseCommentsCommandDalTests : TestBase<int>
    {
        #region ResetCaseCommentsAsync
        
        [Test]
        public async Task ResetCaseCommentsAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[dev_reset_case_comments_by_ExaminerId]";
            var expectedExaminerUserId = Create<int>();
            var expectedParams =
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ResetCaseCommentsCommandDto>());
        
            var sut = new ResetCaseCommentsCommandDal(sqlManager);
            await sut.ResetCaseCommentsAsync(expectedExaminerUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
