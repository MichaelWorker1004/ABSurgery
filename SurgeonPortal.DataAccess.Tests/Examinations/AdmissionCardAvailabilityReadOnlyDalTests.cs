using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.DataAccess.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations
{
	public class AdmissionCardAvailabilityReadOnlyDalTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_adm_card_availability]";
            var expectedUserID = Create<int>();
            var expectedExamID = Create<int>();
            var expectedParams = 
                new
                {
                    UserID = expectedUserID,
                    ExamID = expectedExamID,
                };
            
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<AdmissionCardAvailabilityReadOnlyDto>());
        
            var sut = new AdmissionCardAvailabilityReadOnlyDal(sqlManager);
            await sut.GetByExamIdAsync(
                expectedUserID,
                expectedExamID);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AdmissionCardAvailabilityReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdmissionCardAvailabilityReadOnlyDal(sqlManager);
            var result = await sut.GetByExamIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
