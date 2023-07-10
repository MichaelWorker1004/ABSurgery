using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.DataAccess.Scoring.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring.CE
{
	public class ExamineeReadOnlyDalTests : TestBase<string>
    {
        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examinee_session_byid]";
            var expectedExamScheduleId = Create<int>();
            var expectedParams =
                new
                {
                    ExamScheduleId = expectedExamScheduleId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ExamineeReadOnlyDto>());
        
            var sut = new ExamineeReadOnlyDal(sqlManager);
            await sut.GetByIdAsync(expectedExamScheduleId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ExamineeReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamineeReadOnlyDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
