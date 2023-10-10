using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.DataAccess.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Picklists
{
	public class FellowshipProgramReadOnlyDalTests : TestBase<int>
    {
        #region GetAllAsync
        
        [Test]
        public async Task GetAllAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_fellowship_program]";
            var expectedFellowshipType = Create<string>();
            var expectedParams =
                new
                {
                    FellowshipType = expectedFellowshipType,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<FellowshipProgramReadOnlyDto>());
        
            var sut = new FellowshipProgramReadOnlyDal(sqlManager);
            await sut.GetAllAsync(expectedFellowshipType);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetAllAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<FellowshipProgramReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new FellowshipProgramReadOnlyDal(sqlManager);
            var result = await sut.GetAllAsync(Create<string>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
