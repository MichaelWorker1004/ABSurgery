using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.CE;
using SurgeonPortal.DataAccess.Contracts.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.CE
{
	public class ExamScoreDalTests : TestBase<int>
    {
        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_exam_schedule_scores]";
            var expectedExamScheduleScoreId = Create<int>();
            var expectedExaminerUserId = Create<int>();
            var expectedParams =
                new
                {
                    ExamScheduleScoreId = expectedExamScheduleScoreId,
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ExamScoreDto>());
        
            var sut = new ExamScoreDal(sqlManager);
            await sut.GetByIdAsync(
                expectedExamScheduleScoreId,
                expectedExaminerUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ExamScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamScoreDal(sqlManager);
            var result = await sut.GetByIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_exam_schedule_scores]";
            var expectedDto = Create<ExamScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamScoreDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    ExamScheduleId = expectedDto.ExamScheduleId,
                    ExaminerUserId = expectedDto.ExaminerUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ExamScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamScoreDal(sqlManager);
            var result = await sut.InsertAsync(Create<ExamScoreDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_exam_schedule_scores]";
            var expectedDto = Create<ExamScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamScoreDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    ExamScheduleScoreId = expectedDto.ExamScheduleScoreId,
                    ExaminerScore = expectedDto.ExaminerScore,
                    ExaminerUserId = expectedDto.ExaminerUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ExamScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamScoreDal(sqlManager);
            var result = await sut.UpdateAsync(Create<ExamScoreDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
