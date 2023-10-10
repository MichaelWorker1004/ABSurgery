using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.DataAccess.Scoring.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring.CE
{
	public class TitleReadOnlyDalTests : TestBase<int>
    {
        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examinee_case_titles]";
            var expectedExamScheduleId = Create<int>();
            var expectedParams =
                new
                {
                    ExamScheduleId = expectedExamScheduleId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<TitleReadOnlyDto>());
        
            var sut = new TitleReadOnlyDal(sqlManager);
            await sut.GetByIdAsync(expectedExamScheduleId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<TitleReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new TitleReadOnlyDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
