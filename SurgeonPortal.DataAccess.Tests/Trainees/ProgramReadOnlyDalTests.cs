using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Trainees;
using SurgeonPortal.DataAccess.Trainees;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Trainees
{
	public class ProgramReadOnlyDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_programs]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ProgramReadOnlyDto>());
        
            var sut = new ProgramReadOnlyDal(sqlManager);
            await sut.GetByUserIdAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ProgramReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ProgramReadOnlyDal(sqlManager);
            var result = await sut.GetByUserIdAsync();
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
