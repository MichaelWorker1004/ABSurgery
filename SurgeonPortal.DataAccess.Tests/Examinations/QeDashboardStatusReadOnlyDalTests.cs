using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.DataAccess.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations
{
	public class QeDashboardStatusReadOnlyDalTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_qe_all_status_byuserid]";
            var expectedUserId = Create<int>();
            var expectedExamheaderId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                    ExamheaderId = expectedExamheaderId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<QeDashboardStatusReadOnlyDto>());
        
            var sut = new QeDashboardStatusReadOnlyDal(sqlManager);
            await sut.GetByExamIdAsync(
                expectedUserId,
                expectedExamheaderId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<QeDashboardStatusReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new QeDashboardStatusReadOnlyDal(sqlManager);
            var result = await sut.GetByExamIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
