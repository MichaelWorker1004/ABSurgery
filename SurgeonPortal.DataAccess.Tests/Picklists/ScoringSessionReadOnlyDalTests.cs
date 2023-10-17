using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.DataAccess.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Picklists
{
	public class ScoringSessionReadOnlyDalTests : TestBase<int>
    {
        #region GetByKeysAsync
        
        [Test]
        public async Task GetByKeysAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_day_session_picklist]";
            var expectedExaminerUserId = Create<int>();
            var expectedCurrentDate = Create<DateTime>();
            var expectedParams =
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    CurrentDate = expectedCurrentDate,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ScoringSessionReadOnlyDto>());
        
            var sut = new ScoringSessionReadOnlyDal(sqlManager);
            await sut.GetByKeysAsync(
                expectedExaminerUserId,
                expectedCurrentDate);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByKeysAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ScoringSessionReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ScoringSessionReadOnlyDal(sqlManager);
            var result = await sut.GetByKeysAsync(
                Create<int>(),
                Create<DateTime>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
