using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.DataAccess.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Picklists
{
	public class StateReadOnlyDalTests : TestBase<string>
    {
        #region GetByCountryAsync
        
        [Test]
        public async Task GetByCountryAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_picklist_states_bycountry]";
            var expectedCountryCode = Create<string>();
            var expectedParams =
                new
                {
                    CountryCode = expectedCountryCode,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<StateReadOnlyDto>());
        
            var sut = new StateReadOnlyDal(sqlManager);
            await sut.GetByCountryAsync(expectedCountryCode);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByCountryAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<StateReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new StateReadOnlyDal(sqlManager);
            var result = await sut.GetByCountryAsync(Create<string>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
