using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.DataAccess.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.MedicalTraining
{
	public class UserCertificateReadOnlyDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_usercertificates_byuserid]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<UserCertificateReadOnlyDto>());
        
            var sut = new UserCertificateReadOnlyDal(sqlManager);
            await sut.GetByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<UserCertificateReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new UserCertificateReadOnlyDal(sqlManager);
            var result = await sut.GetByUserIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
