using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.DataAccess.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Picklists
{
	public class DegreeReadOnlyDalTests : TestBase<int>
    {
        #region GetAllAsync
        
        [Test]
        public async Task GetAllAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_degree]";
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<DegreeReadOnlyDto>());
        
            var sut = new DegreeReadOnlyDal(sqlManager);
            await sut.GetAllAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassNoParameters());
        }
        
        [Test]
        public async Task GetAllAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<DegreeReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new DegreeReadOnlyDal(sqlManager);
            var result = await sut.GetAllAsync();
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
