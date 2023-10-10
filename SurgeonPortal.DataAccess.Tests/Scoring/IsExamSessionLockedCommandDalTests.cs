using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class IsExamSessionLockedCommandDalTests : TestBase<int>
    {
        #region IsExamSessionLocked
        
        [Test]
        public void IsExamSessionLocked_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_isLocked_by_examId]";
            var expectedExamCaseId = Create<int>();
            var expectedParams =
                new
                {
                    ExamCaseId = expectedExamCaseId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<IsExamSessionLockedCommandDto>());
        
            var sut = new IsExamSessionLockedCommandDal(sqlManager);
            sut.IsExamSessionLocked(expectedExamCaseId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
