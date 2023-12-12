using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.User;
using SurgeonPortal.DataAccess.User;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.User
{
	public class CertificationStatusReadOnlyDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_certification_current_status_byuserid]";
            var expectedUserId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                };
            
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<CertificationStatusReadOnlyDto>());
        
            var sut = new CertificationStatusReadOnlyDal(sqlManager);
            await sut.GetByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CertificationStatusReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CertificationStatusReadOnlyDal(sqlManager);
            var result = await sut.GetByUserIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
