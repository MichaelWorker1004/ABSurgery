using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class ExamSessionLockCommandDalTests : TestBase<int>
    {
        #region LockExamSessionAsync
        
        [Test]
        public async Task LockExamSessionAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[lock_exam_scoring]";
            var expectedExamscheduleId = Create<int>();
            var expectedParams = 
                new
                {
                    ExamscheduleId = expectedExamscheduleId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ExamSessionLockCommandDto>());
        
            var sut = new ExamSessionLockCommandDal(sqlManager);
            await sut.LockExamSessionAsync(expectedExamscheduleId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
