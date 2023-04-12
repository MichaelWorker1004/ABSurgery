using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Surgeons;
using SurgeonPortal.DataAccess.Surgeons;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Surgeons
{
	public class CertificationReadOnlyDalTests : TestBase<string>
    {
        #region GetByAbsIdAsync
        
        [Test]
        public async Task GetByAbsIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_certifications]";
            var expectedAbsId = Create<string>();
            var expectedParams =
                new
                {
                    AbsId = expectedAbsId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<CertificationReadOnlyDto>());
        
            var sut = new CertificationReadOnlyDal(sqlManager);
            await sut.GetByAbsIdAsync(expectedAbsId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByAbsIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CertificationReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CertificationReadOnlyDal(sqlManager);
            var result = await sut.GetByAbsIdAsync(Create<string>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
