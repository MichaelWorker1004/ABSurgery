using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.CE;
using SurgeonPortal.DataAccess.Contracts.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.CE
{
	public class GetExamCasesScoredCommandDalTests : TestBase<int>
    {
        #region GetExamCasesScored
        
        [Test]
        public void GetExamCasesScored_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_cases_scored]";
            var expectedExamScheduleId = Create<int>();
            var expectedExaminerUserId = Create<int>();
            var expectedParams =
                new
                {
                    ExamScheduleId = expectedExamScheduleId,
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<GetExamCasesScoredCommandDto>());
        
            var sut = new GetExamCasesScoredCommandDal(sqlManager);
            sut.GetExamCasesScored(
                expectedExamScheduleId,
                expectedExaminerUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
