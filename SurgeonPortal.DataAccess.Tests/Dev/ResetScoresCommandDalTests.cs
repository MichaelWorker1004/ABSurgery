using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Dev;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Dev
{
	public class ResetScoresCommandDalTests : TestBase<int>
    {
        #region ResetExamScoresAsync
        
        [Test]
        public async Task ResetExamScoresAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[dev_reset_scoring_by_ExaminerId]";
            var expectedExaminerUserId = Create<int>();
            var expectedParams = 
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
        
            var sut = new ResetScoresCommandDal(sqlManager);
            await sut.ResetExamScoresAsync(expectedExaminerUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
