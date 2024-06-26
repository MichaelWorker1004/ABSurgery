using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examiners;
using SurgeonPortal.DataAccess.Examiners;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examiners
{
	public class AgendaReadOnlyDalTests : TestBase<int>
    {
        #region GetByExamHeaderIdAsync
        
        [Test]
        public async Task GetByExamHeaderIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examiner_agenda]";
            var expectedExaminerUserId = Create<int>();
            var expectedExamHeaderId = Create<int>();
            var expectedParams = 
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    ExamHeaderId = expectedExamHeaderId,
                };
            
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<AgendaReadOnlyDto>());
        
            var sut = new AgendaReadOnlyDal(sqlManager);
            await sut.GetByExamHeaderIdAsync(
                expectedExaminerUserId,
                expectedExamHeaderId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamHeaderIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AgendaReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AgendaReadOnlyDal(sqlManager);
            var result = await sut.GetByExamHeaderIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
