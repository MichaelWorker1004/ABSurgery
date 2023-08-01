using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Trainees;
using SurgeonPortal.DataAccess.Trainees;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Trainees
{
	public class RegistrationStatusReadOnlyDalTests : TestBase<string>
    {
        #region GetByExamCodeAsync
        
        [Test]
        public async Task GetByExamCodeAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_registration_open]";
            var expectedExamCode = Create<string>();
            var expectedParams =
                new
                {
                    examCode = expectedExamCode,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<RegistrationStatusReadOnlyDto>());
        
            var sut = new RegistrationStatusReadOnlyDal(sqlManager);
            await sut.GetByExamCodeAsync(expectedExamCode);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamCodeAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<RegistrationStatusReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new RegistrationStatusReadOnlyDal(sqlManager);
            var result = await sut.GetByExamCodeAsync(Create<string>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
