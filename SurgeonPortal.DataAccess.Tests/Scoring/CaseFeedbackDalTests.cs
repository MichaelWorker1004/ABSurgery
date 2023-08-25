using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Scoring
{
	public class CaseFeedbackDalTests : TestBase<string>
    {
        #region DeleteAsync
                
        [Test]
        public async Task DeleteAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[delete_case_feedback_byid]";
            var expectedDto = Create<CaseFeedbackDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            
            var sut = new CaseFeedbackDal(sqlManager);
            await sut.DeleteAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        #endregion

        #region GetByExaminerIdAsync
        
        [Test]
        public async Task GetByExaminerIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_case_feedback_by_examinerId]";
            var expectedExaminerUserId = Create<int>();
            var expectedCaseHeaderId = Create<int>();
            var expectedParams =
                new
                {
                    ExaminerUserId = expectedExaminerUserId,
                    CaseHeaderId = expectedCaseHeaderId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<CaseFeedbackDto>());
        
            var sut = new CaseFeedbackDal(sqlManager);
            await sut.GetByExaminerIdAsync(expectedCaseHeaderId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExaminerIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseFeedbackDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseFeedbackDal(sqlManager);
            var result = await sut.GetByExaminerIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_case_feedback_byid]";
            var expectedId = Create<int>();
            var expectedParams =
                new
                {
                    Id = expectedId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<CaseFeedbackDto>());
        
            var sut = new CaseFeedbackDal(sqlManager);
            await sut.GetByIdAsync(expectedId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseFeedbackDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseFeedbackDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_case_feedback]";
            var expectedDto = Create<CaseFeedbackDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseFeedbackDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    CaseHeaderId = expectedDto.CaseHeaderId,
                    Feedback = expectedDto.Feedback,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseFeedbackDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseFeedbackDal(sqlManager);
            var result = await sut.InsertAsync(Create<CaseFeedbackDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_case_feedback]";
            var expectedDto = Create<CaseFeedbackDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseFeedbackDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
                    CaseHeaderId = expectedDto.CaseHeaderId,
                    Feedback = expectedDto.Feedback,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<CaseFeedbackDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new CaseFeedbackDal(sqlManager);
            var result = await sut.UpdateAsync(Create<CaseFeedbackDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
