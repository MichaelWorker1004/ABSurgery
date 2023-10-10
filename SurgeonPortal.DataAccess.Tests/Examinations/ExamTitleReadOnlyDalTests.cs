using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.DataAccess.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations
{
	public class ExamTitleReadOnlyDalTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_exam_title_byExamId]";
            var expectedExamId = Create<int>();
            var expectedParams =
                new
                {
                    ExamId = expectedExamId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ExamTitleReadOnlyDto>());
        
            var sut = new ExamTitleReadOnlyDal(sqlManager);
            await sut.GetByExamIdAsync(expectedExamId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ExamTitleReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamTitleReadOnlyDal(sqlManager);
            var result = await sut.GetByExamIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
