using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.DataAccess.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.MedicalTraining
{
	public class AdvancedTrainingDalTests : TestBase<int>
    {
        #region GetByTrainingIdAsync
        
        [Test]
        public async Task GetByTrainingIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_advanced_training_by_trainingid]";
            var expectedId = Create<int>();
            var expectedParams =
                new
                {
                    Id = expectedId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<AdvancedTrainingDto>());
        
            var sut = new AdvancedTrainingDal(sqlManager);
            await sut.GetByTrainingIdAsync(expectedId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByTrainingIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AdvancedTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdvancedTrainingDal(sqlManager);
            var result = await sut.GetByTrainingIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_advanced_training]";
            var expectedDto = Create<AdvancedTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdvancedTrainingDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.,
                    TrainingTypeId = expectedDto.TrainingTypeId,
                    ProgramId = expectedDto.ProgramId,
                    Other = expectedDto.Other,
                    StartDate = expectedDto.StartDate,
                    EndDate = expectedDto.EndDate,
                    CreatedByUserId = expectedDto.,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AdvancedTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdvancedTrainingDal(sqlManager);
            var result = await sut.InsertAsync(Create<AdvancedTrainingDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_advanced_training]";
            var expectedDto = Create<AdvancedTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdvancedTrainingDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
                    UserId = expectedDto.,
                    TrainingTypeId = expectedDto.TrainingTypeId,
                    ProgramId = expectedDto.ProgramId,
                    Other = expectedDto.Other,
                    StartDate = expectedDto.StartDate,
                    EndDate = expectedDto.EndDate,
                    LastUpdatedByUserId = expectedDto.,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AdvancedTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdvancedTrainingDal(sqlManager);
            var result = await sut.UpdateAsync(Create<AdvancedTrainingDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
