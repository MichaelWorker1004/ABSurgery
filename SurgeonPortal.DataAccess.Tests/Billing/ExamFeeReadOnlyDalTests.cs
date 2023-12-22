using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Billing;
using SurgeonPortal.DataAccess.Contracts.Billing;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Billing
{
	public class ExamFeeReadOnlyDalTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_exam_fee_by_examId]";
            var expectedUserId = Create<int>();
            var expectedExamId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                    ExamId = expectedExamId,
                };
            
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ExamFeeReadOnlyDto>());
        
            var sut = new ExamFeeReadOnlyDal(sqlManager);
            await sut.GetByExamIdAsync(
                expectedUserId,
                expectedExamId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByExamIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<ExamFeeReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new ExamFeeReadOnlyDal(sqlManager);
            var result = await sut.GetByExamIdAsync(
                Create<int>(),
                Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[Get_Exam_Fees_byUserId]";
            var expectedUserId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(CreateMany<ExamFeeReadOnlyDto>());
        
            var sut = new ExamFeeReadOnlyDal(sqlManager);
            await sut.GetByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDtos = CreateMany<ExamFeeReadOnlyDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecords(expectedDtos);
        
            var sut = new ExamFeeReadOnlyDal(sqlManager);
            var result = await sut.GetByUserIdAsync(Create<int>());
        
            expectedDtos.Should().BeEquivalentTo(
                result,
                nameof(result));
        }
        
        #endregion
	}
}
