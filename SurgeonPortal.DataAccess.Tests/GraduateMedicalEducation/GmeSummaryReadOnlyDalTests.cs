using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.DataAccess.GraduateMedicalEducation;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.GraduateMedicalEducation
{
	public class GmeSummaryReadOnlyDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_gmesummary_byuserid]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<GmeSummaryReadOnlyDto>());
        
            var sut = new GmeSummaryReadOnlyDal(sqlManager);
            await sut.GetByUserIdAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<GmeSummaryReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new GmeSummaryReadOnlyDal(sqlManager);
            var result = await sut.GetByUserIdAsync();
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
