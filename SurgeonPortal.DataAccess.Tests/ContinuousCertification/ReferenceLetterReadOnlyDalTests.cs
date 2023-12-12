using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.ContinuousCertification;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.ContinuousCertification
{
	public class ReferenceLetterReadOnlyDalTests : TestBase<int>
    {
        #region GetAllByUserIdAsync
        
        [Test]
        public async Task GetAllByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_cc_refLetters]";
            var expectedUserId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ReferenceLetterReadOnlyDto>());
        
            var sut = new ReferenceLetterReadOnlyDal(sqlManager);
            await sut.GetAllByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ReferenceLetterReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ReferenceLetterReadOnlyDal(sqlManager);
            var result = await sut.GetAllByUserIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
