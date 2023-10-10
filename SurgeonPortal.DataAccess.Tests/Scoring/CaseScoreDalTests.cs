using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class CaseScoreDalTests : TestBase<int>
    {
        #region DeleteAsync
                
        [Test]
        public async Task DeleteAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[delete_examinerscore_byid]";
            var expectedDto = Create<CaseScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            
            var sut = new CaseScoreDal(sqlManager);
            await sut.DeleteAsync(expectedDto);
        
            var p =
                new
                {
                    ExamScoringId = expectedDto.ExamScoringId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        #endregion

        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_examcasescore_byid]";
            var expectedExamScoringId = Create<int>();
            var expectedExaminerUserId = Create<int>();
            var expectedParams =
                new
                {
                    ExamScoringId = expectedExamScoringId,
                    ExaminerUserId = expectedExaminerUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<CaseScoreDto>());
        
            var sut = new CaseScoreDal(sqlManager);
            await sut.GetByIdAsync(expectedExamScoringId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseScoreDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[upsert_examinerscore]";
            var expectedDto = Create<CaseScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseScoreDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    ExamineeUserId = expectedDto.ExamineeUserId,
                    ExamCaseId = expectedDto.ExamCaseId,
                    Score = expectedDto.Score,
                    Remarks = expectedDto.Remarks,
                    CriticalFail = expectedDto.CriticalFail,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseScoreDal(sqlManager);
            var result = await sut.InsertAsync(Create<CaseScoreDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[upsert_examinerscore]";
            var expectedDto = Create<CaseScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseScoreDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    ExamineeUserId = expectedDto.ExamineeUserId,
                    ExamCaseId = expectedDto.ExamCaseId,
                    Score = expectedDto.Score,
                    Remarks = expectedDto.Remarks,
                    CriticalFail = expectedDto.CriticalFail,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseScoreDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseScoreDal(sqlManager);
            var result = await sut.UpdateAsync(Create<CaseScoreDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
