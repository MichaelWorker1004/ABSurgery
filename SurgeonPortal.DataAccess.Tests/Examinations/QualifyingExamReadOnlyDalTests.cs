using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.DataAccess.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations
{
	public class QualifyingExamReadOnlyDalTests : TestBase<int>
    {
        #region GetAsync
        
        [Test]
        public async Task GetAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_current_qualifying_exam]";
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<QualifyingExamReadOnlyDto>());
        
            var sut = new QualifyingExamReadOnlyDal(sqlManager);
            await sut.GetAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassNoParameters());
        }
        
        [Test]
        public async Task GetAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<QualifyingExamReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new QualifyingExamReadOnlyDal(sqlManager);
            var result = await sut.GetAsync();
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
