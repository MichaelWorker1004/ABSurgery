using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.DataAccess.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Picklists
{
	public class ResidencyProgramReadOnlyDalTests : TestBase<int>
    {
        #region GetAllAsync
        
        [Test]
        public async Task GetAllAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_residency_program]";
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ResidencyProgramReadOnlyDto>());
        
            var sut = new ResidencyProgramReadOnlyDal(sqlManager);
            await sut.GetAllAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassNoParameters());
        }
        
        [Test]
        public async Task GetAllAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ResidencyProgramReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ResidencyProgramReadOnlyDal(sqlManager);
            var result = await sut.GetAllAsync();
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
