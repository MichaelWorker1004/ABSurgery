using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.DataAccess.GraduateMedicalEducation;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.GraduateMedicalEducation
{
	public class RotationGapReadOnlyDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_gme_conflicts]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<RotationGapReadOnlyDto>());
        
            var sut = new RotationGapReadOnlyDal(sqlManager);
            await sut.GetByUserIdAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<RotationGapReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new RotationGapReadOnlyDal(sqlManager);
            var result = await sut.GetByUserIdAsync();
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
