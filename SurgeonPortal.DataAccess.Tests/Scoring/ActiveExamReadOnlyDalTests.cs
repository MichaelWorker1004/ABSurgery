using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class ActiveExamReadOnlyDalTests : TestBase<int>
    {
        #region GetByExaminerUserIdAsync
        
        [Test]
        public async Task GetByExaminerUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_active_exam_byuserid]";
            var expectedExaminerUserId = Create<int>();
            var expectedCurrentDate = Create<DateTime>();
            var expectedParams = 
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    CurrentDate = expectedCurrentDate,
                };
            
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ActiveExamReadOnlyDto>());
        
            var sut = new ActiveExamReadOnlyDal(sqlManager);
            await sut.GetByExaminerUserIdAsync(
                expectedExaminerUserId,
                expectedCurrentDate);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExaminerUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ActiveExamReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ActiveExamReadOnlyDal(sqlManager);
            var result = await sut.GetByExaminerUserIdAsync(
                Create<int>(),
                Create<DateTime>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
