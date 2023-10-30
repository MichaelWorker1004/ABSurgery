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
	public class ExamSessionReadOnlyDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examinee_sessions]";
            var expectedExaminerUserId = Create<int>();
            var expectedExamDate = Create<DateTime>();
            var expectedParams = 
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    ExamDate = expectedExamDate.ToString("yyyy-MM-dd"),
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ExamSessionReadOnlyDto>());
        
            var sut = new ExamSessionReadOnlyDal(sqlManager);
            await sut.GetByUserIdAsync(
                expectedExaminerUserId,
                expectedExamDate);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ExamSessionReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ExamSessionReadOnlyDal(sqlManager);
            var result = await sut.GetByUserIdAsync(
                Create<int>(),
                Create<DateTime>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
