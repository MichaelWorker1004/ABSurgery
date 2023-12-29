using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.DataAccess.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations
{
	public class QeAttestationReadOnlyDalTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_qe_attestation_items_by_userId]";
            var expectedUserId = Create<int>();
            var expectedExamId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                    ExamId = expectedExamId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<QeAttestationReadOnlyDto>());
        
            var sut = new QeAttestationReadOnlyDal(sqlManager);
            await sut.GetByExamIdAsync(
                expectedUserId,
                expectedExamId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<QeAttestationReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new QeAttestationReadOnlyDal(sqlManager);
            var result = await sut.GetByExamIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
