using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.DataAccess.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations
{
	public class ExamOverviewReadOnlyDalTests : TestBase<int>
    {
        #region GetAllAsync
        
        [Test]
        public async Task GetAllAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_exam_overview]";
            var expectedUserId = 1234;
            var expectedParams =
                new
                {
                    UserId = expectedUserId
                };

            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ExamOverviewReadOnlyDto>());
        
            var sut = new ExamOverviewReadOnlyDal(sqlManager);
            await sut.GetAllAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetAllAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ExamOverviewReadOnlyDto>();
            var expectedUserId = 1234;

            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ExamOverviewReadOnlyDal(sqlManager);
            var result = await sut.GetAllAsync(expectedUserId);
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
