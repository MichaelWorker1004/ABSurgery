using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.DataAccess.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Picklists
{
	public class FellowshipTypeReadOnlyDalTests : TestBase<int>
    {
        #region GetAsync
        
        [Test]
        public async Task GetAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_fellowship_types]";
            
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<FellowshipTypeReadOnlyDto>());
        
            var sut = new FellowshipTypeReadOnlyDal(sqlManager);
            await sut.GetAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassNoParameters());
        }
        
        [Test]
        public async Task GetAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<FellowshipTypeReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new FellowshipTypeReadOnlyDal(sqlManager);
            var result = await sut.GetAsync();
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
